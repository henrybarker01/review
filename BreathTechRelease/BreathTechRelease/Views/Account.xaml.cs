using Acr.UserDialogs;
using BreathTechRelease.PopUps;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : Xamarin.Forms.ContentPage
    {
        public Account()
        {
            InitializeComponent();
          //  this.Navigation = navigation;
            string type = Preferences.Get("subscriptionType", "");
            string isSubscriptionActive = Preferences.Get("SubscriptionActive", "");
            //if (type == "Free" || isSubscriptionActive == "0")
                //searchImage.IsVisible = false;
        }

        private async void Search_Pressed(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SearchPopup());
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
            SecureStorage.Remove("oauth_token");
            Preferences.Clear();
        }

        private async void Account_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }
        private async void Subscribe_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Subscribe());
        }
        private async void MediaLibraay_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MediaLibrary());
        }

        private async void Support_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Support());
            await Navigation.PushAsync(new LegalDocumentTemplate("Support", "Support.html","support"));
        }

        private async void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            await Navigation.PushAsync(new LegalDocumentTemplate("Privacy Policy", "PrivacyPolicy.html", ""));
            UserDialogs.Instance.HideLoading();
        }

        private async void AboutUs_Tapped(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            await Navigation.PushAsync(new LegalDocumentTemplate("About Us", "AboutUs.html",""));
            UserDialogs.Instance.HideLoading();
        }

        private async void Terms_Tapped(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            await Navigation.PushAsync(new LegalDocumentTemplate("Terms Of Use", "Terms.html",""));
            UserDialogs.Instance.HideLoading();
            //await Navigation.PushAsync(new MeetTheTeam());
        }
    }
}