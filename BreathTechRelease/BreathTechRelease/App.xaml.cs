using System;
using Xamarin.Forms;
using BreathTechRelease.Views;
using Xamarin.Essentials;
using System.Net;
using Plugin.InAppBilling;

namespace BreathTechRelease
{

    public partial class App : Application
    {
        public const string LoggedInKey = "LoggedIn";
        public const string AppleUserIdKey = "AppleUserIdKey";
        public static string TransId, SubscriptionType, otpEmail;
        public static DateTime SubscriptionEndDate;
        string userId;
        public static bool SubscriptionActivation;
        public static bool isOtpSend = false;
        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental" });
            InitializeComponent();
            Plugin.Media.CrossMedia.Current.Initialize();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //MainPage = new NavigationPage(new MainPage());
            var test = CrossInAppBilling.IsSupported;
            if (test)
            {

            }
            else
            {

            }
            bool internetStatus = CheckNetwork();
            string userId = Preferences.Get("recordid", "");
            if (!string.IsNullOrEmpty(userId))
            {
                if (internetStatus)
                {
                    if (Application.Current.Properties.ContainsKey("sessionExpire"))
                    {
                        var loginSession = Application.Current.Properties["sessionExpire"];
                        DateTime loginExpire = Convert.ToDateTime(loginSession);
                        if (DateTime.Now > loginExpire)
                        {
                            MainPage = new NavigationPage(new Login(true));
                        }
                        else
                        {
                            MainPage = new NavigationPage(new MainPage(false));
                        }
                    }
                    else
                    {
                        MainPage = new NavigationPage(new MainPage(false));
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new NoInternetConnectionView());
                }
            }
            else
            {
                /*if (App.isOtpSend == true)
                    MainPage = new NavigationPage(new ResetPasswordOTP(App.otpEmail));
                else*/
                MainPage = new NavigationPage(new Login());
            }
        }

        protected override async void OnStart()
        {
            /*bool internetStatus = CheckNetwork();
            string userId = Preferences.Get("recordid", "");
            string token = Preferences.Get("auth_token", "");
            if (!string.IsNullOrEmpty(userId))
            {
                if (internetStatus)
                {
                    if (Application.Current.Properties.ContainsKey("sessionExpire"))
                    {
                        var id = Application.Current.Properties["sessionExpire"];
                        DateTime loginExpire = Convert.ToDateTime(id);
                        if (DateTime.Now.AddDays(+3) > loginExpire)
                        {
                            MainPage = new NavigationPage(new Login(true));
                        }
                        else
                        {
                            MainPage = new NavigationPage(new MainPage(false));
                        }
                    }
                    else
                    {
                        MainPage = new NavigationPage(new MainPage(false));
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new NoInternetConnectionView());
                }
            }
            else
            {
                    MainPage = new NavigationPage(new Login());
            }*/
        }

        protected override void OnSleep()
        {
            CheckNetwork();
        }

        protected override void OnResume()
        {
            bool internetStatus = CheckNetwork();
            string userId = Preferences.Get("recordid", "");
            if (!string.IsNullOrEmpty(userId))
            {
                if (internetStatus)
                {
                    //MainPage = new NavigationPage(new MainPage(false));
                }
                else
                {
                    MainPage = new NavigationPage(new NoInternetConnectionView());
                }
            }
            else
            {
                if (App.isOtpSend == true)
                    MainPage = new NavigationPage(new ResetPasswordOTP(App.otpEmail));
                else
                    MainPage = new NavigationPage(new Login());
            }
        }

        public bool CheckNetwork()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                //App.Current.MainPage.DisplayAlert("Alert", "Network is Not available. Please check your internet settings", "ok");
                return false;
            }
        }
    }
}
