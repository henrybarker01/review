using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.Views;
using BreathTechRelease.Constant;
using BreathTechRelease.ViewModels;
using Rg.Plugins.Popup.Extensions;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Subscribe : ContentPage
    {
        private SubscriptionlistViewModel vModel = new SubscriptionlistViewModel();
        public DateTime currentdate = DateTime.Now;
        public Subscribe()
        {
            InitializeComponent();
        }

        private async void Privacy_Policy(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
            vModel?.GoToTermsAndPrivacy(StringConstant.kPrivacyKey);
        }

        private async void Terms_of_Service(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);

            vModel?.GoToTermsAndPrivacy(StringConstant.kTermsKey);
        }
        private async void monthly_Clicked(object sender, EventArgs e)
        {
            if(App.SubscriptionType=="Free")
                await Navigation.PushAsync(new SubscribeOption("ONE MONTH SUBSCRIPTION", "Find your Path for one month.", "Subscribe for R 7.99 / month", false));
            else if (currentdate<=App.SubscriptionEndDate)
            {
                await App.Current.MainPage.DisplayAlert("BreathTech", "You have" + " " + App.SubscriptionType + " " + "active which ends on" + " " + App.SubscriptionEndDate.ToString("MMM dd, yyyy"), "OK");
            }
            else
                await Navigation.PushAsync(new SubscribeOption("ONE MONTH SUBSCRIPTION", "Find your Path for one month.", "Subscribe for R 7.99 / month", false));
        }
        private async void yearly_Clicked(object sender, EventArgs e)
        {
            if (App.SubscriptionType == "Free")
                await Navigation.PushAsync(new SubscribeOption("ONE YEAR SUBSCRIPTION", "Find your Path for one year.", "Subscribe for R 70.99 / year", true));
            else if (currentdate <= App.SubscriptionEndDate)
            {
                await App.Current.MainPage.DisplayAlert("BreathTech", "You have" + " " + App.SubscriptionType + " " + "active which ends on" + " " + App.SubscriptionEndDate.ToString("MMM dd, yyyy"), "OK");
            }
            else
                await Navigation.PushAsync(new SubscribeOption("ONE YEAR SUBSCRIPTION", "Find your Path for one year.", "Subscribe for R 70.99 / year", true));
        }
    }    
}
