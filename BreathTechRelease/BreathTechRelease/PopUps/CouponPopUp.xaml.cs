using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.Models;
using BreathTechRelease.Views;
using BreathTechRelease.Service;
using Xamarin.Essentials;
using BreathTechRelease.ResponseModels;
using Acr.UserDialogs;
using BreathTechRelease.Manager;
using BreathTechRelease.Authentication;

namespace BreathTechRelease.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CouponPopUp : PopupPage
    {
        private SubscriptionType subscription;
        public CouponPopUp(SubscriptionType subscriptionData)
        {
            InitializeComponent();
            this.subscription = subscriptionData;
        }

        private async void promoButton_Clicked(object sender, EventArgs e)
        {

            string promoText = promoEntry.Text;
            try
            {
                var apiService = NetworkService.GetApiService();

                var auth_token = Preferences.Get("auth_token", "");

                var RecordId = Preferences.Get("recordid", null);
                if(string.IsNullOrEmpty(promoText))
                {
                    ErrorMsg.Text = "Please Enter Promocode";
                }
                else
                {
                    bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                    if (isInternetConnectionEnabled)
                    {
                        UserDialogs.Instance.ShowLoading("Loading");
                        var data = await BreathtechAPIManager.ValidatePromocodeForUser(auth_token, Int32.Parse(RecordId), promoText, subscription.subscriptionTypeId);
                        if (data.issuccess == true && data!= null)
                        {
                            await DisplayAlert("Success", data.message, "Okay");

                            DateTime renewaleDate = DateTime.Now;
                            if (subscription.subscriptionTypeId == 2)
                                renewaleDate = DateTime.Now.AddMonths(1);
                            else if (subscription.subscriptionTypeId == 3)
                                renewaleDate = DateTime.Now.AddYears(1);

                            var result = await App.Current.MainPage.DisplayAlert("Confirm Your Subscription", "Thank you for subscribing to Breathtech. This subscription of " + data.payableAmount + " is recurring and will automatically renew on " + renewaleDate.ToString("dd MMM yyyy") +
                            ", cancel anytime.", "Confirm", "Cancel");

                            if (result == true)
                            {
                                PopupNavigation.PopAsync();
                               // Navigation.PushAsync(new Paygate(subscription.subscriptionTypeId.ToString(), data.payableAmount, data.promoCodeId));
                            }

                        }
                        else if(data.issuccess== false && data!= null)
                        {
                            await DisplayAlert("Error", data.message, "Okay");

                        }
                        else
                        {
                            await DisplayAlert("Error", "Something went wrong", "Okay");
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                    }
                }

                
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong", "Okay");
            }
        }

        private async void skip_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.PopAsync(true);
            DateTime renewaleDate = DateTime.Now;
            if (subscription.subscriptionTypeId == 2)
                renewaleDate = DateTime.Now.AddMonths(1);
            else if (subscription.subscriptionTypeId == 3)
                renewaleDate = DateTime.Now.AddYears(1);

            var result = await App.Current.MainPage.DisplayAlert("Confirm Your Subscription", "Thank you for subscribing to Breathtech. This subscription of " + subscription.amount + " is recurring and will automatically renew on " + renewaleDate.ToString("dd MMM yyyy") +
                            ", cancel anytime.", "Confirm", "Cancel");

            if (result == true)
            {
                PopupNavigation.PopAsync();
                //Navigation.PushAsync(new Paygate(subscription.subscriptionTypeId.ToString(), subscription.amount, 0));
            }
        }
    }
}