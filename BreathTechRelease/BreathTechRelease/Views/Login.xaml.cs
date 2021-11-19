using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Authentication;
using Xamarin.Essentials;

using System.Diagnostics;
using System.Net;

using Newtonsoft.Json;
using Xamarin.Auth;

using OAuthNativeFlow;
using BreathTechRelease.Models;
using System.Text.RegularExpressions;
using BreathTechRelease.Manager;
using BreathTechRelease.RequestModels;
using Acr.UserDialogs;
using BreathTechRelease.ViewModels;
using System.Security.Cryptography;
using BreathTechRelease.Helpers;
using BreathTechRelease.Service;
using BreathTechRelease.ResponseModels;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : Xamarin.Forms.ContentPage
    {
        // public IncapIntergration.BTFunctions.androidAuthentication usrAuth = new IncapIntergration.BTFunctions.androidAuthentication();

        string status = null;
        public string GoogleIconText { get; set; }
        int count = 0;
        GetUserDetailByIdResponseModel userDetail;

        public Login()
        {
            InitializeComponent();

            CheckLoginStatus();
            BindingContext = new LoginViewModel();

        }
        public Login(bool tokenExpired)
        {
            InitializeComponent();
            if (tokenExpired)
            {
                DisplayAlert("BreathTech", "Session Expired", "OK");
            }
            BindingContext = new LoginViewModel();
        }
        protected override bool OnBackButtonPressed()
        {
            var login = new Login();
            bool result = true;
            if (App.Current.MainPage == login)
            {
                result = false;
            }

            return result;
        }


        public async void CheckLoginStatus()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                status = oauthToken;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }





        private async void VerificationMail_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ResendVerificationMail());
        }
        private async void ResetPassword_Tapped(object sender, EventArgs e)
        {
            if (App.isOtpSend == true)
                await App.Current.MainPage.Navigation.PushAsync(new ResetPasswordOTP(App.otpEmail));
            else
                await App.Current.MainPage.Navigation.PushAsync(new ResetPassword());
        }

        private async void CreateAccountGesture_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var email = Email.Text.Trim().ToLower();
            var emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var password = Password.Text;

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    ErrorMsg.Text = "Email Field cannot be empty";
                }
                else if (string.IsNullOrEmpty(password))
                {
                    ErrorMsg.Text = "Password Field cannot be empty";
                }
                else if (password.Length <= 3)
                {
                    ErrorMsg.Text = "Password length must be greater than 3 characters";
                }
                else
                {

                    if (Regex.IsMatch(email, emailPattern))
                    {
                        bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                        if (isInternetConnectionEnabled)
                        {
                            UserDialogs.Instance.ShowLoading("Loading");
                            //await SecureStorage.SetAsync("oauth_token", "logedin");

                            // Create Hash value for password
                            string hash = "";
                            hash = Encryption.EncryptString(NetworkService.Key, password);
                            LoginRequestModel loginRequestModel = new LoginRequestModel();
                            loginRequestModel.username = email;
                            loginRequestModel.password = hash;
                            var result = await BreathtechAPIManager.Login(loginRequestModel);
                            UserDialogs.Instance.HideLoading();
                            if (result != null && result.issuccess == true)
                            {
                                Preferences.Set("subscriptionType", result.userDetails.subscriptionType);
                                Preferences.Set("customer_id", result.userDetails.customerId);
                                Preferences.Set("auth_token", "Bearer " + result.token);
                                Preferences.Set("Email", result.userDetails.email);
                                Preferences.Set("SubscriptionExpiryDate", result.userDetails.subscriptionEndDate.ToString("dd/MM/yyyy"));
                                Preferences.Set("SubscriptionActive", result.userDetails.isSubcriptionActive.ToString());
                                Preferences.Set("recordid", result.userDetails.recordID.ToString());
                                Preferences.Set("UID", result.userDetails.uid);
                                Application.Current.Properties["sessionExpire"] = DateTime.Now.AddDays(+2);
                                await Application.Current.SavePropertiesAsync();


                                //await Navigation.PushAsync(new Welcome());
                                //await Navigation.PushAsync(new MainPage(true));
                                UpdateVidePlayed();

                                //await DisplayAlert("Success", result.outMSG, "Continue");
                                ToastConfig toastConfig = new ToastConfig("Logged in succesfully");
                                toastConfig.SetDuration(3000);
                                toastConfig.SetBackgroundColor(Color.Gray);
                                UserDialogs.Instance.Toast(toastConfig);

                                //await Navigation.PushAsync(new SubscriptionListView());
                            }
                            else if (result != null && result.issuccess == false)
                            {
                                await DisplayAlert("Error", result.outMSG, "Cancel");
                            }
                            else
                            {
                                await Navigation.PushAsync(new MaintenanceAlert());
                            }

                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                        }

                    }
                    else
                    {
                        ErrorMsg.Text = "Invalid Email";
                    }
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //ErrorMsg.Text = "Error:" + ex;
                await DisplayAlert("Error", "Something went wrong", "Cancel");
            }

        }

        //[Obsolete]
        //private async void GoogleLogin_Clicked(object sender, EventArgs e)
        //{



        //    string clientId = null;
        //    string redirectUri = null;



        //    switch (Device.RuntimePlatform)
        //    {
        //        case Device.iOS:
        //            clientId = Constants.iOSClientId;
        //            redirectUri = Constants.iOSRedirectUrl;
        //            break;

        //        case Device.Android:
        //            clientId = Constants.AndroidClientId;
        //            redirectUri = Constants.AndroidRedirectUrl;
        //            break;
        //    }



        //    var accountStoreAccounts = AccountStore.Create().FindAccountsForService(Constants.AppName);

        //    await SecureStorageAccountStore.MigrateAllAccountsAsync(Constants.AppName, accountStoreAccounts);

        //    var authenticator = new OAuth2Authenticator(
        //        clientId,
        //        null,
        //        Constants.Scope,
        //        new Uri(Constants.AuthorizeUrl),
        //        new Uri(redirectUri),
        //        new Uri(Constants.AccessTokenUrl),
        //        null,
        //        true);

        //    authenticator.Completed += OnAuthCompleted;
        //    authenticator.Error += OnAuthError;

        //    AuthenticationState.Authenticator = authenticator;

        //    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
        //    presenter.Login(authenticator);
        //}

        //[Obsolete]

        public async void UpdateVidePlayed()
        {

            try
            {
                UserDialogs.Instance.ShowLoading();
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    var apiService = NetworkService.GetApiService();

                    var auth_token = Preferences.Get("auth_token", "");

                    var RecordId = Preferences.Get("recordid", null);



                    var data = await BreathtechAPIManager.GetUserDetailById(auth_token, Int32.Parse(RecordId));

                    if (data != null && data.isSuccess == true)
                    {
                        userDetail = new GetUserDetailByIdResponseModel();
                        userDetail = data;

                        if (data.userData.isVideoPlayed == true)
                        {
                            await DisplayAlert("", "Skiping video because its already played", "ok");
                            await Navigation.PushAsync(new MainPage(false));
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new MainPage(true));
                            //await Navigation.PushAsync(new MainPage(true));
                        }

                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Alert", data.message, "Cancel");
                    }

                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "OK");
                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }

        }
        //public async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        //{

        //    var authenticator = sender as OAuth2Authenticator;
        //    if (authenticator != null)
        //    {
        //        authenticator.Completed -= OnAuthCompleted;
        //        authenticator.Error -= OnAuthError;
        //    }

        //    if (e.IsAuthenticated)
        //    {
        //        // If the user is authenticated, request their basic user data from Google
        //        // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
        //        var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
        //        var response = await request.GetResponseAsync();

        //        if (response != null)
        //        {
        //            // Deserialize the data and store it in the account store
        //            // The users email address will be used to identify data in SimpleDB
        //            var JsonStr = await response.GetResponseTextAsync();
        //            var usr = JsonConvert.DeserializeObject<User>(JsonStr);
        //            var PropScheme = typeof(User).GetProperties();

        //            for (int i = 0; i < PropScheme.Length; i++)
        //            {
        //                string value = isEmpty((string)usr.GetType().GetProperty(PropScheme[i].Name).GetValue(usr, null));
        //                usr.GetType().GetProperty(PropScheme[i].Name).SetValue(usr, value);
        //            }


        //            try
        //            {
        //                OsmosysRestAPI osmosysRestAPI = new OsmosysRestAPI();
        //                osmosysRestAPI.INSBTUGUsers(usr.GivenName, usr.FamilyName, "NA", usr.Email, usr.Id, usr.Email, "NA", "NA", usr.VerifiedEmail,
        //                   "NA", "NA", "NA", "NA", "NA", 2);
        //                await SecureStorage.SetAsync("oauth_token", usr.Id);
        //                GoogleLoginRequestModel googleLoginRequestModel = new GoogleLoginRequestModel();
        //                googleLoginRequestModel.Email = usr.Email;
        //                googleLoginRequestModel.GoogleId = usr.Id;
        //                var result = await BreathtechAPIManager.GoogleLogin(googleLoginRequestModel);
        //                await Navigation.PushAsync(new Intro());
        //            }
        //            catch (Exception ex)
        //            {
        //                await DisplayAlert("Error", "Possible that device doesn't support secure storage on device." + ex + "", "Ok");
        //            }

        //        }
        //    }
        //}

        //void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        //{
        //    var authenticator = sender as OAuth2Authenticator;
        //    if (authenticator != null)
        //    {
        //        authenticator.Completed -= OnAuthCompleted;
        //        authenticator.Error -= OnAuthError;
        //    }
        //    Debug.WriteLine("Authentication error: " + e.Message);
        //}

        string isEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "NA";
            }
            return value;
        }
    }
}