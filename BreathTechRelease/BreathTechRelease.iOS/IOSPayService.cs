using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BraintreeApplePay;
using BraintreeCore;
using BraintreeDropIn;
using BraintreeUIKit;
using BreathTechRelease.ViewModels;
using Foundation;
using PassKit;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BreathTechRelease.iOS.IOSPayService))]
namespace BreathTechRelease.iOS
{
    public class IOSPayService : PKPaymentAuthorizationViewControllerDelegate, IPayService
    {
        bool isDropUI = false;
        string _clientToken;
        TaskCompletionSource<string> payTcs;
        TaskCompletionSource<DropUIResult> dropUiPayTcs;
        PKPaymentAuthorizationViewController pKPaymentAuthorizationViewController;

        public event EventHandler<DropUIResult> OnDropUISuccessful = delegate { };
        public event EventHandler<string> OnDropUIError = delegate { };

        public event EventHandler<string> OnTokenizationSuccessful = delegate { };
        public event EventHandler<string> OnTokenizationError = delegate { };

        bool isReady;
        BTAPIClient braintreeClient;

        public bool CanPay
        {
            get
            {
                return isReady;
            }
        }

        public string clientToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double total_amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<DropUIResult> ShowDropUI(double totalPrice, string merchantId, int resultCode = 1234)
        {
            dropUiPayTcs = new TaskCompletionSource<DropUIResult>();
            if (CanPay)
            {
                BTDropInRequest request = new BTDropInRequest();
                request.Amount = $"{totalPrice}";
                BTDropInController bTDropInController = new BTDropInController(_clientToken, request, async (controller, result, error) =>
                {
                    if (error == null)
                    {
                        if (result.Cancelled)
                        {
                            dropUiPayTcs.SetCanceled();
                        }
                        else if (result.PaymentOptionType == BTUIKPaymentOptionType.ApplePay)
                        {

                            try
                            {
                                isDropUI = true;
                                var nonce = await TokenizePlatform(totalPrice, merchantId);

                                var dropResult = new DropUIResult()
                                {
                                    Nonce = nonce ?? string.Empty,
                                    Type = $"{BTUIKPaymentOptionType.ApplePay}"
                                };
                                OnDropUISuccessful?.Invoke(this, dropResult);
                                dropUiPayTcs.TrySetResult(dropResult);
                            }
                            catch (TaskCanceledException)
                            {
                                dropUiPayTcs.SetCanceled();
                            }
                            catch (Exception exception)
                            {
                                OnDropUIError?.Invoke(this, exception.Message);
                                dropUiPayTcs.TrySetException(exception);
                            }
                            finally
                            {
                                pKPaymentAuthorizationViewController?.DismissViewController(true, null);
                                isDropUI = false;
                            }


                        }
                        else
                        {
                            var dropResult = new DropUIResult()
                            {
                                Nonce = result.PaymentMethod?.Nonce ?? string.Empty,
                                Type = $"{result.PaymentOptionType}"
                            };
                            OnDropUISuccessful?.Invoke(this, dropResult);
                            dropUiPayTcs.TrySetResult(dropResult);
                        }

                    }
                    else
                    {
                        OnDropUIError?.Invoke(this, error.Description);
                        dropUiPayTcs.TrySetException(new Exception(error.Description));
                    }


                    controller.DismissViewController(true, null);
                });

                var window = UIApplication.SharedApplication.KeyWindow;
                var _viewController = window.RootViewController;
                while (_viewController.PresentedViewController != null)
                    _viewController = _viewController.PresentedViewController;

                _viewController?.PresentViewController(bTDropInController, true, null);
            }
            else
            {
                OnDropUIError?.Invoke(this, "Platform is not ready to accept payments");
                dropUiPayTcs.TrySetException(new Exception("Platform is not ready to accept payments"));

            }
            return await dropUiPayTcs.Task;
        }


        public async Task<string> TokenizePlatform(double totalPrice, string merchantId)
        {
            payTcs = new TaskCompletionSource<string>();
            if (isReady)
            {
                var applePayClient = new BTApplePayClient(braintreeClient);
                applePayClient.PaymentRequest((request, error) =>
                {

                    if (error == null)
                    {
                        RequestPaymentAuthorization(request, new Dictionary<string, double>{
                               { "My App",totalPrice}
                         }, merchantId);
                    }
                    else
                    {
                        if (!isDropUI)
                        {
                            OnTokenizationError?.Invoke(this, "Error: Couldn't create payment request.");
                        }

                        payTcs.TrySetException(new Exception("Error: Couldn't create payment request."));

                    }
                });

            }
            else
            {
                if (!isDropUI)
                {
                    OnTokenizationError?.Invoke(this, "Platform is not ready to accept payments");
                }
                payTcs.TrySetException(new Exception("Platform is not ready to accept payments"));

            }

            return await payTcs.Task;
        }

        void RequestPaymentAuthorization(PKPaymentRequest paymentRequest, IDictionary<string, double> summaryItems, string merchantId)
        {
            UserDialogs.Instance.ShowLoading("Loading");

            paymentRequest.MerchantIdentifier = merchantId;
            paymentRequest.MerchantCapabilities = PKMerchantCapability.ThreeDS;
            paymentRequest.CountryCode = "US";
            paymentRequest.CurrencyCode = "USD";

            if (summaryItems != null)
            {
                paymentRequest.PaymentSummaryItems = summaryItems.Select(i => new PKPaymentSummaryItem()
                {
                    Label = i.Key,
                    Amount = new NSDecimalNumber(i.Value)
                }).ToArray();
            }

            var window = UIApplication.SharedApplication.KeyWindow;
            var _viewController = window.RootViewController;
            while (_viewController.PresentedViewController != null)
                _viewController = _viewController.PresentedViewController;


            pKPaymentAuthorizationViewController = new PKPaymentAuthorizationViewController(paymentRequest);
            UserDialogs.Instance.HideLoading();
            if (pKPaymentAuthorizationViewController != null)
            {
                pKPaymentAuthorizationViewController.Delegate = this;
                _viewController?.PresentViewController(pKPaymentAuthorizationViewController, true, null);
            }
            else
            {
                if (!isDropUI)
                {
                    OnTokenizationError?.Invoke(this, "Error: Payment request is invalid.");
                }

                payTcs?.SetException(new Exception("Error: Payment request is invalid."));

            }
        }


        public override void DidAuthorizePayment(PKPaymentAuthorizationViewController controller, PKPayment payment, Action<PKPaymentAuthorizationStatus> completion)
        {
            var applePayClient = new BTApplePayClient(braintreeClient);
            applePayClient.TokenizeApplePayPayment(payment, (tokenizedApplePayPayment, error) =>
            {
                if (error == null)
                {
                    if (string.IsNullOrEmpty(tokenizedApplePayPayment.Nonce))
                    {
                        payTcs?.SetCanceled();

                    }
                    else
                    {
                        if (!isDropUI)
                        {
                            OnTokenizationSuccessful?.Invoke(this, tokenizedApplePayPayment.Nonce);
                        }

                        payTcs?.TrySetResult(tokenizedApplePayPayment.Nonce);
                    }

                    completion(PKPaymentAuthorizationStatus.Success);
                }
                else
                {
                    if (!isDropUI)
                    {
                        OnTokenizationError?.Invoke(this, "Error - Payment tokenization failed");
                    }

                    payTcs?.TrySetException(new Exception("Error - Payment tokenization failed"));

                    completion(PKPaymentAuthorizationStatus.Failure);
                }
            });
        }

        public override void PaymentAuthorizationViewControllerDidFinish(PKPaymentAuthorizationViewController controller)
        {
            controller.DismissViewController(true, null);
        }

        public override void WillAuthorizePayment(PKPaymentAuthorizationViewController controller)
        {

        }

        public Task<bool> InitializeAsync(string paymentClientToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> TokenizeCard(string panNumber = "4111111111111111", string expirationMonth = "12", string expirationYear = "2018", string cvv = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> TokenizePayPal()
        {
            throw new NotImplementedException();
        }
    }
}
