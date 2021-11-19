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
    public partial class PrivacyPolicy : Xamarin.Forms.ContentPage
    {
        public PrivacyPolicy()
        {
            InitializeComponent();
        }

       private async void Continue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}