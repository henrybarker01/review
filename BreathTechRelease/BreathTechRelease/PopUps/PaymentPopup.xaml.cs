using Rg.Plugins.Popup.Pages;
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

namespace BreathTechRelease.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPopup : PopupPage
    {
        private SubscriptionlistViewModel vModel = new SubscriptionlistViewModel();
        public PaymentPopup(SubscriptionlistViewModel Vm = null)
        {
            InitializeComponent();
            //vModel = Vm;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private async void Suscribe_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        private async void Privacy_Policy(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new LegalDocumentTemplate("Privacy Policy", "PrivacyPolicy.html"));
            await PopupNavigation.PopAsync(true);
            vModel?.GoToTermsAndPrivacy(StringConstant.kPrivacyKey);

        }

        private async void Terms_of_Service(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LegalDocumentTemplate("Terms Of Use", "Terms.html"));
            await PopupNavigation.PopAsync(true);
            
            vModel?.GoToTermsAndPrivacy(StringConstant.kTermsKey);
        }

        async void OnClickMonthly(System.Object sender, System.EventArgs e)
        {
           await Navigation.PushPopupAsync(new SubscriptionInfoPopup("One Month Subscription ", " In this subscription we provide full access of Cornerstone , Basic skills, Everyday Breath and find your path for 1 month", "R9.99"));
        }
        async void OnClickYearly(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new SubscriptionInfoPopup("One Year Subscription ", "In this subscription we provide full access of Cornerstone , Basic skills, Everyday Breath and find your path for 1 year", "R19.99"));
        }

    }
}