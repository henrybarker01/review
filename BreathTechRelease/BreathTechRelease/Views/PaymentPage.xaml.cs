using BreathTechRelease.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : Xamarin.Forms.ContentPage
    {

        public PaymentPage(string subscription_ID, double amount)
        {
            try
            {
                InitializeComponent();
                PaymentPageViewModel vm = new PaymentPageViewModel(subscription_ID, amount);
                BindingContext = vm;
            }
            catch (Exception ex)
            {

            }
        }
    }
}