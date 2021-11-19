using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Wallet;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BreathTechRelease.ViewModels;
using Com.Braintreepayments.Api;
using Com.Braintreepayments.Api.Dropin;
using Com.Braintreepayments.Api.Exceptions;
using Com.Braintreepayments.Api.Interfaces;
using Com.Braintreepayments.Api.Models;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(BreathTechRelease.Droid.AndroidPayService))]
namespace BreathTechRelease.Droid
{
    public class AndroidPayService : Java.Lang.Object, IPayService, IBraintreeResponseListener, IBraintreeErrorListener, IPaymentMethodNonceCreatedListener, IBraintreeCancelListener
    {

        static int _requestCode;
        static TaskCompletionSource<DropUIResult> dropUiPayTcs;
        static AndroidPayService CurrentInstance;
        TaskCompletionSource<string> payTcs;
        BraintreeFragment mBraintreeFragment;
        TaskCompletionSource<bool> initializeTcs;


        public bool isReady = true;
        public bool CanPay { get { return isReady; } }

        public string clientToken { get; set; }
        public double total_amount { get; set; }

        //public Action<object, string> OnTokenizationSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public Action<object, string> OnTokenizationError { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<DropUIResult> OnDropUISuccessful;
        public event EventHandler<string> OnDropUIError;
        public event EventHandler<string> OnTokenizationSuccessful;
        public event EventHandler<string> OnTokenizationError;

        public async Task<DropUIResult> ShowDropUI(double totalPrice, string merchantId, int requestCode = 1234)
        {
            if (isReady)
            {

                CurrentInstance = this;
                _requestCode = requestCode;
                dropUiPayTcs = new TaskCompletionSource<DropUIResult>();
                GooglePaymentRequest googlePaymentRequest = new GooglePaymentRequest();

                googlePaymentRequest.InvokeTransactionInfo(TransactionInfo.NewBuilder()
                                                           .SetTotalPrice($"{totalPrice}")
                .SetTotalPriceStatus(WalletConstants.TotalPriceStatusFinal)
                .SetCurrencyCode("USD")
                .Build());

                DropInRequest dropInRequest = new DropInRequest().ClientToken(clientToken)
                                                                 .Amount($"{totalPrice}")
                                                                 .InvokeGooglePaymentRequest(googlePaymentRequest);

                CrossCurrentActivity.Current.Activity.StartActivityForResult(dropInRequest.GetIntent(CrossCurrentActivity.Current.Activity), requestCode);
            }
            else
            {
                OnDropUIError?.Invoke(this, "Platform is not ready to accept payments");
                dropUiPayTcs.TrySetException(new System.Exception("Platform is not ready to accept payments"));

            }

            return await dropUiPayTcs.Task;
        }

        void SetDropResult(DropUIResult dropResult)
        {
            OnDropUISuccessful?.Invoke(this, dropResult);
            dropUiPayTcs?.TrySetResult(dropResult);
        }

        void SetDropException(Exception exception)
        {
            OnDropUIError?.Invoke(this, exception.Message);
            dropUiPayTcs?.TrySetException(exception);
        }

        void SetDropCanceled()
        {
            dropUiPayTcs?.TrySetCanceled();
        }

        public static void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == _requestCode)
            {
                if (resultCode == Result.Ok)
                {
                    DropInResult result = data.GetParcelableExtra(DropInResult.ExtraDropInResult).JavaCast<DropInResult>();
                    var dropResult = new DropUIResult()
                    {
                        Nonce = result.PaymentMethodNonce.Nonce,
                        Type = $"{result.PaymentMethodType}"
                    };

                    CurrentInstance?.SetDropResult(dropResult);
                }
                else if (resultCode == Result.Canceled)
                {
                    CurrentInstance?.SetDropCanceled();
                }
                else
                {
                    Exception error = data.GetSerializableExtra(DropInActivity.ExtraError).JavaCast<Java.Lang.Exception>();
                    CurrentInstance?.SetDropException(error);
                }
            }
        }

        public void OnCancel(int requestCode)
        {
            // Use this to handle a canceled activity, if the given requestCode is important.
            // You may want to use this callback to hide loading indicators, and prepare your UI for input
            payTcs.SetCanceled();
            mBraintreeFragment.RemoveListener(this);
        }

        public async Task<bool> InitializeAsync(string clientToken)
        {
            try
            {
                initializeTcs = new TaskCompletionSource<bool>();
                mBraintreeFragment = BraintreeFragment.NewInstance(CrossCurrentActivity.Current.Activity, clientToken);

                GooglePayment.IsReadyToPay(mBraintreeFragment, this);
            }
            catch (InvalidArgumentException e)
            {
                initializeTcs.TrySetException(e);
            }
            return await initializeTcs.Task;
        }

        public async Task<string> TokenizeCard(string panNumber = "4111111111111111", string expirationMonth = "12", string expirationYear = "2018", string cvv = null)
        {
            if (isReady)
            {
                payTcs = new TaskCompletionSource<string>();
                CardBuilder cardBuilder = new CardBuilder()
                    .CardNumber(panNumber).JavaCast<CardBuilder>()
                    .ExpirationMonth(expirationMonth).JavaCast<CardBuilder>()
                    .ExpirationYear(expirationYear).JavaCast<CardBuilder>()
                    .Cvv(cvv).JavaCast<CardBuilder>();

                mBraintreeFragment.AddListener(this);

                //Card.Tokenize(mBraintreeFragment, cardBuilder);
            }
            else
            {
                OnTokenizationError?.Invoke(this, "Platform is not ready to accept payments");
                payTcs.TrySetException(new System.Exception("Platform is not ready to accept payments"));

            }
            return await payTcs.Task;
        }


        public async Task<string> TokenizePlatform(double totalPrice, string merchantId)
        {
            payTcs = new TaskCompletionSource<string>();
            if (isReady)
            {
                GooglePaymentRequest googlePaymentRequest = new GooglePaymentRequest();

                googlePaymentRequest.InvokeTransactionInfo(TransactionInfo.NewBuilder()
                                                           .SetTotalPrice($"{totalPrice}")
                .SetTotalPriceStatus(WalletConstants.TotalPriceStatusFinal)
                .SetCurrencyCode("USD")
                .Build());

                mBraintreeFragment.AddListener(this);
                GooglePayment.RequestPayment(mBraintreeFragment, googlePaymentRequest);
            }
            else
            {
                OnTokenizationError?.Invoke(this, "Platform is not ready to accept payments");
                payTcs.TrySetException(new System.Exception("Platform is not ready to accept payments"));

            }

            return await payTcs.Task;
        }

        public async Task<string> TokenizePayPal()
        {
            payTcs = new TaskCompletionSource<string>();
            if (isReady)
            {
                mBraintreeFragment.AddListener(this);
                PayPal.AuthorizeAccount(mBraintreeFragment);
            }
            else
            {
                OnTokenizationError?.Invoke(this, "Platform is not ready to accept payments");
                payTcs.TrySetException(new System.Exception("Platform is not ready to accept payments"));

            }
            return await payTcs.Task;
        }

        public void OnPaymentMethodNonceCreated(PaymentMethodNonce paymentMethodNonce)
        {
            // Send this nonce to your server
            string nonce = paymentMethodNonce.Nonce;
            mBraintreeFragment.RemoveListener(this);
            OnTokenizationSuccessful?.Invoke(this, nonce);
            payTcs?.TrySetResult(nonce);

        }

        public void OnResponse(Java.Lang.Object parameter)
        {
            if (parameter is Java.Lang.Boolean)
            {
                var res = parameter.JavaCast<Java.Lang.Boolean>();
                isReady = res.BooleanValue();
                initializeTcs?.TrySetResult(res.BooleanValue());

            }
        }

        public void OnError(Java.Lang.Exception error)
        {
            if (error is ErrorWithResponse)
            {
                ErrorWithResponse errorWithResponse = (ErrorWithResponse)error;
                BraintreeError cardErrors = errorWithResponse.ErrorFor("creditCard");
                if (cardErrors != null)
                {
                    // There is an issue with the credit card.
                    BraintreeError expirationMonthError = cardErrors.ErrorFor("expirationMonth");
                    if (expirationMonthError != null)
                    {
                        // There is an issue with the expiration month.
                        //SetErrorMessage(expirationMonthError.GetMessage());
                        OnTokenizationError?.Invoke(this, expirationMonthError.Message);
                        payTcs?.TrySetException(new System.Exception(expirationMonthError.Message));

                    }
                    else
                    {
                        OnTokenizationError?.Invoke(this, cardErrors.Message);
                        payTcs?.TrySetException(new System.Exception(cardErrors.Message));

                    }
                }
            }

            mBraintreeFragment.RemoveListener(this);
        }
    }

}