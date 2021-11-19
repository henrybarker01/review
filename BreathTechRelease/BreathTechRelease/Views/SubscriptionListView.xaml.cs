using System;
using System.Collections.Generic;
using BreathTechRelease.ViewModels;
using Xamarin.Forms;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.RequestModels;
using BreathTechRelease.Service;
using Xamarin.Essentials;
using BreathTechRelease.Manager;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.PopUps;
using BreathTechRelease.Constant;

namespace BreathTechRelease.Views
{
    public partial class SubscriptionListView : ContentPage
    {


        GetUserDetailByIdResponseModel getUserDetailByIdResponseModel = new GetUserDetailByIdResponseModel();
        private SubscriptionlistViewModel vM = null;
        public SubscriptionListView()
        {
            InitializeComponent();
            //ObserveTermsAndPrivacy();
            

            INavigation navigation = this.Navigation;
            vM = new SubscriptionlistViewModel(navigation);
            vM.IsPageloaded = false;
            BindingContext = vM;

            //CancelSubscriptonLayout.IsVisible = false;
            //DisplayAlert("Confirm Your Subscription","Do you want to subscribe to $ for $ monthly or for $ yearly? +" +
            //    "This subscription will atomatically renew untill cancelled.","Confirm","Cancel");

            ButtonVisibility();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!vM.IsPageloaded)
            {
                //PaymentPopup();
            }
        }

        private void ObserveTermsAndPrivacy() {
           /* MessagingCenter.Subscribe<PaymentPopup>(this, StringConstant.kTermsKey, (Obj) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new LegalDocumentTemplate("Terms Of Use", "Terms.html",""));
                });
            });

            MessagingCenter.Subscribe<PaymentPopup>(this, StringConstant.kPrivacyKey, (Obj) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new LegalDocumentTemplate("Privacy Policy", "PrivacyPolicy.html",""));
                });
            });*/
        }

        public async void PaymentPopup()
        {
           // await PopupNavigation.PushAsync(new PaymentPopup(vM));
        }

        private async void CancelSubscriptionButton_Clicked(object sender, EventArgs e)
        {

           
            CancelAutoSubscription cancelAutoSubscription = new CancelAutoSubscription();
            try
            {
                var apiService = NetworkService.GetApiService();

                var auth_token = Preferences.Get("auth_token", "");

                var uid = Preferences.Get("UID", null);

                var RecordId = Preferences.Get("recordid", null);

                var data = await apiService.CancelAutoSubscription(auth_token, uid);
                
               

                if(data.issuccess==true)
                {
                    await DisplayAlert("Success", "Successfully Unsubscribed", "Okay");
                    //await DisplayAlert("Success", ""+cancelAutoSubscription.message, "Okay");
                    
                }
                else
                {
                    await DisplayAlert("Error","Something went wrong", "Okay");

                }
            }
            catch(Exception)
            {

            }

             
        }

        public async void ButtonVisibility()
        {
            var apiService = NetworkService.GetApiService();

            var auth_token = Preferences.Get("auth_token", "");

            var RecordId = Preferences.Get("recordid", null);



            var data = await BreathtechAPIManager.GetUserDetailById(auth_token, Int32.Parse(RecordId));


            try
            {
                if (data.userData.subscriptionType == "Free")
                {
                    CancelSubscriptonLayout.IsVisible = false;
                }
                else
                {
                    CancelSubscriptonLayout.IsVisible = true;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
