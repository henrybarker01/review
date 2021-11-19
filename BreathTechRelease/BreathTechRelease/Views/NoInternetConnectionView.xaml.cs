using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BreathTechRelease.Views
{
    public partial class NoInternetConnectionView : ContentPage
    {
        public NoInternetConnectionView()
        {
            InitializeComponent();
        }
        void btnTryAgain_Clicked(System.Object sender, System.EventArgs e)
        {
            bool internetStatus = checkNetworkConnectivity();
            string userId = Preferences.Get("recordid", "");
            if (!string.IsNullOrEmpty(userId))
            {
                if (internetStatus)
                {

                    App.Current.MainPage = new NavigationPage(new MainPage(false));
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new NoInternetConnectionView());
                }
            }
            else
                App.Current.MainPage = new NavigationPage(new Login());
        }
        public bool checkNetworkConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                DisplayAlert("Alert", "Network is Not available. Please check your internet settings", "ok");
                return false;
            }
        }
    }
}
