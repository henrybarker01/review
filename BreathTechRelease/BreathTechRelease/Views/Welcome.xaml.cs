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
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private async void tutorial(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Intro());
        }

        private async void breathe(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(false));
        }
    }
}