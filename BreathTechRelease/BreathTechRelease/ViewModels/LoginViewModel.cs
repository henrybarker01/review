using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.Models;
using BreathTechRelease.RequestModels;
using BreathTechRelease.Views;
using Newtonsoft.Json;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Acr.UserDialogs;
using BreathTechRelease.Service;

namespace BreathTechRelease.ViewModels
{

    public class LoginViewModel
    {
        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        public ICommand LoginCmd { get; set; }

        public bool IsAppleSignInAvailable { get { return appleSignInService?.IsAvailable ?? false; } }
        public ICommand SignInWithAppleCommand { get; set; }
        IAppleSignInService appleSignInService;

        //asdadasda
        public LoginViewModel()
        {
            LoginCmd = new Command(async () => await LoginGoogleAsync());
            appleSignInService = DependencyService.Get<IAppleSignInService>();
            SignInWithAppleCommand = new Command(OnAppleSignInRequest);
        }

        async void OnAppleSignInRequest()
        {
            appleSignInService.SignIn();
            MessagingCenter.Unsubscribe<object, AppleAccount>(this, "AppleAccountDetails");
            MessagingCenter.Subscribe<object, AppleAccount>(this, "AppleAccountDetails", async (sender, accountDetails) =>
             {

                 AppleLoginRequestModel appleLogin = new AppleLoginRequestModel();
                 appleLogin.appleUserId = accountDetails.UserId;
                 appleLogin.email = accountDetails.Email == null ? "" : accountDetails.Email;
                 appleLogin.fisrtName = accountDetails.Name;
                 appleLogin.lastName = "";
                 appleLogin.realUserStatus = accountDetails.RealUserStatus;
                 appleLogin.appleToken = accountDetails.Token;
                 var result = await BreathtechAPIManager.AppleLogin(appleLogin);
                 if (result != null && result.isSuccess == true)
                 {

                     Preferences.Set("subscriptionType", result.userDetails.subscriptionType);
                     Preferences.Set("customer_id", result.userDetails.customerId);
                     Preferences.Set("recordid", result.userDetails.recordID.ToString());
                     Preferences.Set("uid", result.userDetails.uid);
                     Preferences.Set("auth_token", "Bearer " + result.token);
                     Preferences.Set("Email", result.userDetails.email);
                     Preferences.Set("SubscriptionExpiryDate", result.userDetails.subscriptionEndDate.ToString("dd/MM/yyyy"));
                     Preferences.Set("SubscriptionActive", result.userDetails.isSubcriptionActive.ToString());
                     Application.Current.Properties["sessionExpire"] = DateTime.Now.AddDays(+2);
                     await Application.Current.SavePropertiesAsync();
                     App.Current.MainPage = new NavigationPage(new MainPage(true));
                     //await App.Current.MainPage.Navigation.PushAsync(new MainPage(true));
                     //App.Current.MainPage = new NavigationPage(new UpdateProfileView(true));
                     //App.Current.MainPage.DisplayAlert("Success", result.message, "Continue");
                     ToastConfig toastConfig = new ToastConfig("Logged in succesfully");
                     toastConfig.SetDuration(3000);
                     toastConfig.SetBackgroundColor(Color.Gray);
                     UserDialogs.Instance.Toast(toastConfig);

                 }
                 else if (result != null && result.isSuccess == false)
                 {
                     App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                     //await Navigation.PushAsync(new MaintenanceAlert());
                 }
                 else
                 {
                     App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                 }
             });

            /*var account = await appleSignInService.SignInAsync();
            if (account != null)
            {
                System.Diagnostics.Debug.WriteLine($"Signed in!\n  Name: {account?.Name ?? string.Empty}\n  Email: {account?.Email ?? string.Empty}\n  UserId: {account?.UserId ?? string.Empty}");
                
                   
                      var result = await BreathtechAPIManager.AppleLogin(appleLogin);
                      if(result!=null)
                      {
                        Preferences.Set(App.LoggedInKey, true);
                        await SecureStorage.SetAsync(App.AppleUserIdKey, account.UserId);
                        Preferences.Set("recordid", account.UserId.ToString());
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                    }
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                    }

                OnSignIn?.Invoke(this, default(EventArgs));
            }*/

        }

        /*public event EventHandler OnSignIn = async delegate
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Intro());
        };*/

        async Task LoginGoogleAsync()
        {
            try
            {
                AuthNetwork authNetwork = new AuthNetwork()
                {
                    Name = "Google",
                    Icon = "ic_google",
                    Foreground = "#000000",
                    Background = "#F8F8F8"
                };

                if (!string.IsNullOrEmpty(_googleService.AccessToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name,
                                Email = e.Data.Email
                            };
                            GoogleLoginRequestModel googleLoginRequestModel = new GoogleLoginRequestModel();
                            googleLoginRequestModel.GoogleId = socialLoginData.Id;
                            googleLoginRequestModel.UserName = socialLoginData.Name;
                            googleLoginRequestModel.Email = socialLoginData.Email;
                            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                            if (isInternetConnectionEnabled)
                            {
                                //UserDialogs.Instance.ShowLoading("Loading");
                                var result = await BreathtechAPIManager.GoogleLogin(googleLoginRequestModel);
                                //UserDialogs.Instance.HideLoading();
                                if (result != null && result.isSuccess == true)
                                {

                                    Preferences.Set("subscriptionType", result.userDetails.subscriptionType);
                                    Preferences.Set("customer_id", result.userDetails.customerId);
                                    Preferences.Set("recordid", result.userDetails.recordID.ToString());
                                    Preferences.Set("uid", result.userDetails.uid);
                                    Preferences.Set("auth_token", "Bearer " + result.token);
                                    Preferences.Set("Email", result.userDetails.email);
                                    Preferences.Set("SubscriptionExpiryDate", result.userDetails.subscriptionEndDate.ToString("dd/MM/yyyy"));
                                    Preferences.Set("SubscriptionActive", result.userDetails.isSubcriptionActive.ToString());
                                    Application.Current.Properties["sessionExpire"] = DateTime.Now.AddDays(+2);
                                    await Application.Current.SavePropertiesAsync();
                                    //await App.Current.MainPage.Navigation.PushAsync(new Intro());
                                    /* if (string.IsNullOrEmpty(result.userDetails.email) || string.IsNullOrEmpty(result.userDetails.fname) || string.IsNullOrEmpty(result.userDetails.lname))
                                     {
                                         App.Current.MainPage = new NavigationPage(new UpdateProfileView(true));
                                     }
                                     else*/
                                    App.Current.MainPage = new NavigationPage(new MainPage(true));
                                    //await App.Current.MainPage.Navigation.PushAsync(new MainPage(true));

                                    //App.Current.MainPage.DisplayAlert("Success", result.message, "Continue");
                                    ToastConfig toastConfig = new ToastConfig("Logged in succesfully");
                                    toastConfig.SetDuration(3000);
                                    toastConfig.SetBackgroundColor(Color.Gray);
                                    UserDialogs.Instance.Toast(toastConfig);

                                }
                                else if (result != null && result.isSuccess == false)
                                {
                                    await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                                    //await Navigation.PushAsync(new MaintenanceAlert());
                                }
                                else
                                {
                                    await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                                }
                            }
                            else
                            {
                                App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                            }



                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                //UserDialogs.Instance.HideLoading();
                //Debug.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }


        }

    }
}
