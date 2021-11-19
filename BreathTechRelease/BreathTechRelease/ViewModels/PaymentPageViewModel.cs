using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BreathTechRelease.ViewModels
{
    public enum PaymentOptionEnum
    {
        CreditCard,
        DropUI,
        PayPal,
        Platform
    }

    public class PaymentPageViewModel : INotifyPropertyChanged
    {
        public ICommand PayCommand { get; set; }
        public ICommand OnPaymentOptionSelected { get; set; }
        public PaymentOptionEnum PaymentOptionEnum { get; set; }
        string customer_id;
        string auth_token;
        string _subscription_ID;
        double _amount;

        IPayService _payService;

        string paymentClientToken;
        const string MerchantId = "qk473xm39bstwppw";
        
        public CardInfo CardInfo { get; set; } = new CardInfo();

        public PaymentPageViewModel(string subscription_id, double amount)
        {
            _subscription_ID = subscription_id;
            _amount = amount;
            _payService = Xamarin.Forms.DependencyService.Get<IPayService>();

            PayCommand = new Command(async () => await CreatePayment());
            OnPaymentOptionSelected = new Command<PaymentOptionEnum>((data) =>
            {
                PaymentOptionEnum = data;

                if (PaymentOptionEnum != PaymentOptionEnum.CreditCard)
                    PayCommand.Execute(null);
            });

            customer_id = Preferences.Get("customer_id", "");
            auth_token = Preferences.Get("auth_token", "");
            GetPaymentConfig();

            _payService.OnTokenizationSuccessful += OnTokenizationSuccessful;
            _payService.OnTokenizationError += OnTokenizationError;
            _payService.OnDropUISuccessful += OnDropUISuccessful;
            _payService.OnTokenizationError += OnDropUIError;
        }

        async Task GetPaymentConfig()
        {
            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
            if (isInternetConnectionEnabled)
            {
                var result = await BreathtechAPIManager.GetClientToken(auth_token, customer_id);
                if (result != null && result.statusCode == 200)
                {
                    paymentClientToken = result.clientToken;
                    _payService.clientToken = paymentClientToken;
                }
                await _payService.InitializeAsync(paymentClientToken);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
            }
        }

        async Task CreatePayment()
        {
            UserDialogs.Instance.ShowLoading("Loading");

            if (true)
            {
                try
                {
                    switch (PaymentOptionEnum)
                    {
                        //case PaymentOptionEnum.Platform:
                        //    await _payService.TokenizePlatform(AmountToPay, MerchantId);
                        //    break;
                        case PaymentOptionEnum.CreditCard:
                            await _payService.TokenizeCard(CardInfo.CardNumber.Replace(" ", string.Empty), CardInfo.Expiry.Substring(0, 2), $"{DateTime.Now.ToString("yyyy").Substring(0, 2)}{CardInfo.Expiry.Substring(3, 2)}", CardInfo.Cvv);
                            break;
                        case PaymentOptionEnum.PayPal:
                            await _payService.TokenizePayPal();
                            break;
                        case PaymentOptionEnum.DropUI:
                            UserDialogs.Instance.HideLoading();
                            await _payService.ShowDropUI(_amount, MerchantId);
                            break;
                        default:
                            break;
                    }
                }
                catch (TaskCanceledException ex)
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Error", "Processing was cancelled", "Ok");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");
                    System.Diagnostics.Debug.WriteLine(ex);
                }

            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Error", "Payment not available", "Ok");
                });
            }
        }

        async void OnDropUIError(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine(e);
            await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");
        }

        async void OnDropUISuccessful(object sender, DropUIResult e)
        {
            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
            if (isInternetConnectionEnabled)
            {
                var result = await BreathtechAPIManager.MakePayment(auth_token, customer_id, _amount, e.Nonce.ToString(), e.Type.ToString(), _subscription_ID);
                if (result != null && result.issuccess == true)
                {
                    System.Diagnostics.Debug.WriteLine($"Payment Authorized - {e.Nonce} by {e.Type}");
                    await App.Current.MainPage.DisplayAlert("Success", $"Payment Authorized: the token is {e.Nonce} by {e.Type}", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Payment Failed", "OK");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
            }
            
        }

        async void OnTokenizationSuccessful(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine($"Payment Authorized - {e}");
            UserDialogs.Instance.HideLoading();
            await App.Current.MainPage.DisplayAlert("Success", $"Payment Authorized: the token is {e}", "Ok");
            App.Current.MainPage.Navigation.PopAsync();
        }

        async void OnTokenizationError(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine(e);
            UserDialogs.Instance.HideLoading();
            await App.Current.MainPage.DisplayAlert("Error", "Unable to process payment", "Ok");
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }

}
