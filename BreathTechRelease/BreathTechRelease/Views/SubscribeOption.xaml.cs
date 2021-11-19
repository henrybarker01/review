using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Constant;
using BreathTechRelease.ViewModels;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscribeOption : ContentPage
    {
        private SubscriptionlistViewModel vModel = new SubscriptionlistViewModel();
        public SubscribeOption(string subscriptionLen, string subscriptionText, string buttonText, bool IsYearly)
        {
            InitializeComponent();
            this.BindingContext = vModel;
            OptionLength.Text = subscriptionLen;
            OptionText.Text = subscriptionText;
            subscribeText.Text = buttonText;
            vModel.IsYearly = IsYearly;
        }

        private void btn_Clicked(object sender, EventArgs e)
        {

        }

        private async void Terms_of_Service(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);

            vModel?.GoToTermsAndPrivacy(StringConstant.kTermsKey);

        }

        private async void Privacy_Policy(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
            vModel?.GoToTermsAndPrivacy(StringConstant.kPrivacyKey);
        }
    }
}