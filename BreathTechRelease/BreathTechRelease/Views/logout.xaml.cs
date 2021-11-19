using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class logout : ContentPage
    {
        public logout()
        {
            InitializeComponent();


            SecureStorage.Remove("oauth_token");
            SecureStorage.Remove(App.AppleUserIdKey);
            Preferences.Clear();
            
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.MainPage = new NavigationPage(new Login());
        }


    }
}