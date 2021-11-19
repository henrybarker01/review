using System;
using BreathTechRelease.Manager;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using BreathTechRelease.ResponseModels;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using BreathTechRelease.Views;
using BreathTechRelease.Authentication;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.PopUps;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using BreathTechRelease.RequestModels;
using System.Linq;
using BreathTechRelease.Constant;

namespace BreathTechRelease.ViewModels
{
    public class SubscriptionlistViewModel : BaseViewModel
    {
        private string subscriptionExpiryDate;
        public string SubscriptionExpiryDate
        {
            get => subscriptionExpiryDate;
            set
            {
                subscriptionExpiryDate = value;
                OnPropertyChanged(nameof(SubscriptionExpiryDate));
            }
        }

        private bool isYearly;
        public bool IsYearly
        {
            get => isYearly;
            set
            {
                isYearly = value;
                OnPropertyChanged(nameof(IsYearly));
            }
        }

        private bool isPageloaded;
        public bool IsPageloaded
        {
            get => isPageloaded;
            set
            {
                isPageloaded = value;
                OnPropertyChanged(nameof(IsPageloaded));
            }
        }

        private string currentSubscriptionText;
        public string CurrentSubscriptionText
        {
            get => currentSubscriptionText;
            set
            {
                currentSubscriptionText = value;
                OnPropertyChanged(nameof(CurrentSubscriptionText));
            }
        }
        public ObservableCollection<SubscriptionType> subscriptionList;
        private bool _listIsVisisble;
        private bool _labelIsVisible;
        private INavigation _navigation;

        public bool listIsVisisble
        {
            get => _listIsVisisble;
            set
            {
                _listIsVisisble = value;
                OnPropertyChanged(nameof(listIsVisisble));
            }
        }

        public bool labelIsVisible
        {
            get => _labelIsVisible;
            set
            {
                _labelIsVisible = value;
                OnPropertyChanged(nameof(labelIsVisible));
            }
        }

        public ObservableCollection<SubscriptionType> SubscriptionList
        {
            get { return subscriptionList; }
            set
            {
                subscriptionList = value;
                OnPropertyChanged(nameof(SubscriptionList));
            }
        }

        public ICommand BuySubscriptionCommand => new Command(BuySubscriptionCommandExecution);

        private async void BuySubscriptionCommandExecution()
        {
            try
            {
                //PromoPopup(obj);
                //if (Device.RuntimePlatform == Device.iOS)
                //{
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading();
                        IsPageloaded = true;
                        /*var id1 = "324234";                            //var token ="dsfsdfdsfsd";
                        var state1 = "Purchased";
                        var email1 = Preferences.Get("Email", string.Empty);
                        InsertTransactionRequestModel insertTransactionRequest1 = new InsertTransactionRequestModel()
                        {
                            email = email1,
                            subscriptionId = "2",
                            applePurchaseId = id1,
                            applePurchaseToken = "",
                            applePurchaseState = state1
                        };
                        string auth_token1 = Preferences.Get("auth_token", "");
                        var result1 = await BreathtechAPIManager.InsertTransactionApple(auth_token1, insertTransactionRequest1);
                        if (result1 != null && result1.issuccess == true)
                        {
                            Preferences.Set("TransactionId", result1.transactionId);
                            App.TransId = result1.transactionId.ToString();
                            await App.Current.MainPage.DisplayAlert("Alert", "Transaction deatils inserted successfully", "Ok");
                            return;
                        }
                        */
                        string productId = null;
                        if (!IsYearly)
                        {
                            if (Device.RuntimePlatform == Device.iOS)
                                productId = "onemonthsubscriptions";
                            else
                                productId = "onemonthsubscription";
                        }
                        else //if (obj.subscriptionTypeId == 3)
                        {
                            if (Device.RuntimePlatform == Device.iOS)
                                productId = "oneyearsubscriptions";
                            else
                                productId = "oneyearsubscription";
                        }
                        var Purchase = CrossInAppBilling.Current;
                        Purchase.InTestingMode = true;
                        var connected = await Purchase.ConnectAsync(ItemType.Subscription);
                        if (!connected)
                        {
                            //Couldn't connect to billing, could be offline, alert user
                            return;
                        }

                        //try to purchase item
                        try
                        {
                            string payload = "";
                            Preferences.Set("productId", productId);
                            if (Device.RuntimePlatform == Device.Android)
                                payload = "payload";

                            var purchase = await Purchase.PurchaseAsync(productId, ItemType.Subscription, payload);
                            if (purchase == null)
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Purchase are null", "Ok");
                                //Not purchased, alert the user
                            }
                            else //if(Device.RuntimePlatform == Device.iOS)
                            {
                                var id = purchase.Id;
                                Preferences.Set("Id", purchase.Id);
                                var token = purchase.PurchaseToken;
                                Preferences.Set("PurchaseToken", purchase.PurchaseToken);
                                var state = purchase.State.ToString();
                                Preferences.Set("State", purchase.State.ToString());
                                var email = Preferences.Get("Email", string.Empty);
                                InsertTransactionRequestModel insertTransactionRequest = new InsertTransactionRequestModel()
                                {
                                    email = email,
                                    subscriptionId = (IsYearly) ? "3" : "2",//obj.subscriptionTypeId.ToString(),
                                    applePurchaseId = id,
                                    applePurchaseToken = token,
                                    applePurchaseState = state,
                                    isAppleTransaction = (Device.RuntimePlatform == Device.iOS) ? true : false
                                };
                                string auth_token = Preferences.Get("auth_token", "");
                                UserDialogs.Instance.ShowLoading();
                                var result = await BreathtechAPIManager.InsertTransactionApple(auth_token, insertTransactionRequest);
                                if (result != null && result.issuccess == true)
                                {
                                    Preferences.Set("TransactionId", result.transactionId);
                                    await App.Current.MainPage.DisplayAlert("Alert", "Transaction details inserted successfully", "Ok");
                                    UserDialogs.Instance.HideLoading();
                                    App.Current.MainPage = new NavigationPage(new MainPage(false));
                                }
                                else
                                {
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Something bad has occurred, alert user
                    }
                    finally
                    {
                        //App.Current.MainPage.DisplayAlert("Alert", "Finally bloack", "Ok");
                        //Disconnect, it is okay if we never connected
                        await CrossInAppBilling.Current.DisconnectAsync();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                }
                /*}
                else
                {
                    var result = await App.Current.MainPage.DisplayAlert("Confirm Your Subscription", "Do you want to subscribe to $ for $"+ obj.amount + " monthly or for $" + obj.amount + " yearly? " +
                    "This subscription will atomatically renew untill cancelled.", "Confirm", "Cancel");
                    if(result == true)
                    {
                        _navigation.PushAsync(new Paygate(obj.subscriptionTypeId.ToString(), obj.amount,0));
                    }
                }*/
            }
            catch (Exception ex)
            {
            }
        }

        public async void PromoPopup(SubscriptionType subscription)
        {
            //await PopupNavigation.PushAsync(new CouponPopUp(subscription));
        }

        public SubscriptionlistViewModel()
        {
        }
        public SubscriptionlistViewModel(INavigation navigation)
        {
            try
            {
                _navigation = navigation;
                SubscriptionExpiryDate = Preferences.Get("SubscriptionExpiryDate", "");
                string isSubscriptionActive = Preferences.Get("SubscriptionActive", "");
                if (isSubscriptionActive == "1")
                    CurrentSubscriptionText = "Already Subscribed";
                else
                    CurrentSubscriptionText = "Subscription End Date";
                GetData();
            }
            catch (Exception ex)
            {

            }
        }

        async void GetData()
        {
            try
            {
                var auth_token = Preferences.Get("auth_token", null);
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    UserDialogs.Instance.ShowLoading("Loading");
                    var result = await BreathtechAPIManager.GetSubscriptionTypes(auth_token);
                    UserDialogs.Instance.HideLoading();
                    if (result != null && result.issuccess == true)
                    {
                        listIsVisisble = true;
                        labelIsVisible = false;
                        SubscriptionList = new ObservableCollection<SubscriptionType>(result.subscriptionType);
                        //SubscriptionList = new ObservableCollection<SubscriptionlistViewModel>(list);
                    }
                    else if (result != null && result.issuccess == false)
                    {
                        listIsVisisble = false;
                        labelIsVisible = true;
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                    }

                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Cancel");
            }
        }

        public void GoToTermsAndPrivacy(string Key = null)
        {
            if (Key != null)
            {
                if (Key == StringConstant.kTermsKey)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new LegalDocumentTemplate("Terms Of Use", "Terms.html", ""));
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new LegalDocumentTemplate("Privacy Policy", "PrivacyPolicy.html", ""));
                    });
                }

            }
        }
    }
}
