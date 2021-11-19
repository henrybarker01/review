using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.ResponseModels;
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
using BreathTechRelease.RequestModels;
using BreathTechRelease.Manager;
using Acr.UserDialogs;
using System.Security.Cryptography;
using BreathTechRelease.Helpers;
using BreathTechRelease.Service;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.PopUps;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Rg.Plugins.Popup.Extensions;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : Xamarin.Forms.ContentPage
    {

        // public IncapIntergration.BTFunctions.androidAuthentication usrAuth = new IncapIntergration.BTFunctions.androidAuthentication();
        private int countryId;

        public int CountryId
        {
            get 
            { 
                return countryId; 
            }
            set 
            { 
                countryId = value;
                OnPropertyChanged("CountryId");
            }
        }

        private string countryCode;

        public string CountryCode
        {
            get 
            { 
                return countryCode; 
            }
            set 
            { 
                countryCode = value;
                OnPropertyChanged("CountryCode");
            }
        }

        private ImageSource countryFlag;

        public ImageSource CountryFlag
        {
            get 
            { 
                return countryFlag; 
            }
            set 
            { 
                countryFlag = value;
                OnPropertyChanged("CountryFlag");
            }
        }
        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        OsmosysRestAPI osmosysRestAPI = new OsmosysRestAPI();
        public Registration()
        {
            InitializeComponent();
            BindingContext = this;
            CountryFlag = "za.png";
            //flagImage.Source = "za.png";
            CountryCode = "+27";
            CountryId = 314;
            dob.MaximumDate = DateTime.Now;
            MessagingCenter.Subscribe<object>(this, "Hi", (e) => {
                var date = dob.Date;
                dob.Date = date;
            });
            GetData();
            GetAllCountriesPopup();
        }
        private async void GetData()
        {
            try
            {
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    UserDialogs.Instance.ShowLoading("Loading");
                    var result = await BreathtechAPIManager.GetAllGender();
                    if (result != null && result.issuccess == true)
                    {
                        gender.ItemsSource = result.lstGender.ToList();
                    }
                    UserDialogs.Instance.HideLoading();

                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "OK");
                }

            }

            catch(Exception ex)
            {

            }
        }

        private async void btnRegistration_Clicked(object sender, EventArgs e)
        {
            var email = TbEmail.Text.Trim().ToLower();
            var emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //var number = TbMobile.Text.Replace(" ","").Replace("-","");
            //var numberPattern = @"\+?[0-9]";
            var numberPattern = "^[0-9]{9,12}$";
            var FName = TbFName.Text;
            var LName = TbLName.Text;
            var Number = TbMobile.Text;
            var Password = TbPassword.Text;
            var confrimPass = TbConfirmPass.Text;
            var gen = gender.SelectedItem as LstGender;
            var dobirth = dob.Date;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,12}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            // Create Hash value for password
            string hash = Encryption.EncryptString(NetworkService.Key, Password);

            try
            {
                if (string.IsNullOrEmpty(FName))
                {
                    ErrorMsg.Text = "First name cannot be empty";
                }
                else if (string.IsNullOrEmpty(LName))
                {
                    ErrorMsg.Text = "Last name cannot be empty";
                }
                else if (string.IsNullOrEmpty(Number))
                {
                    ErrorMsg.Text = "Mobile number cannot be empty";
                }
                else if(Number.StartsWith("0"))
                {
                    ErrorMsg.Text = "Mobile number cannot start from zero";
                }
                else if (!string.IsNullOrEmpty(Number)&&!Regex.IsMatch(Number, numberPattern))
                {
                    ErrorMsg.Text = "Invalid mobile number";
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ErrorMsg.Text = "Email address cannot be empty";
                }
                else if(!Regex.IsMatch(email,emailPattern))
                {
                    ErrorMsg.Text = "Invalid email address";
                }
                else if(dobirth==DateTime.MinValue)
                {
                    ErrorMsg.Text = "Select Date of birth";
                }
                else if(gen == null)
                {
                   ErrorMsg.Text = "Select gender";
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    ErrorMsg.Text = "Password cannot be empty";
                }
                /*else if (Password.Length < 5)
                {
                    ErrorMsg.Text = "Password Length must be atleast 5 characters";
                }*/
                else if (!hasLowerChar.IsMatch(Password))
                {
                    ErrorMsg.Text = "Password should contain at least one lower case letter";
                }
                else if (!hasUpperChar.IsMatch(Password))
                {
                    ErrorMsg.Text = "Password should contain at least one upper letter";
                }
                else if (!hasNumber.IsMatch(Password))
                {
                    ErrorMsg.Text = "Password should contain at least one number";
                }
                else if (!hasSymbols.IsMatch(Password))
                {
                    ErrorMsg.Text = "Password should contain at least one special character";
                }
                else if (!hasMiniMaxChars.IsMatch(Password))
                {
                    ErrorMsg.Text = "Password should not be lesser than 8 or greater than 12 characters";
                }
                else if (string.IsNullOrEmpty(confrimPass))
                {
                    ErrorMsg.Text = "Confirm Password cannot be empty";
                }
                else if (confrimPass.Length < 8)
                {
                    ErrorMsg.Text = "Confirm Password Length must be atleast 8 characters";
                }
                else if (TbPassword.Text != TbConfirmPass.Text)
                {
                    ErrorMsg.Text = "Password and Confirm Password must be the same";
                }
                else if(CheckBoxAgreeTerms.IsChecked == false)
                {
                    ErrorMsg.Text = "Please accept Terms and Conditions";
                }
                else
                {
                    ErrorMsg.Text = string.Empty;
                    if (Regex.IsMatch(email, emailPattern))
                    {
                        bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                        if (isInternetConnectionEnabled)
                        {
                            UserDialogs.Instance.ShowLoading("Loading");
                            RegisterUserRequestModel registerUserRequestModel = new RegisterUserRequestModel();
                            registerUserRequestModel.fname = FName;
                            registerUserRequestModel.lname = LName;
                            if(!string.IsNullOrEmpty(Number))
                            {
                                if (CountryCode != null)
                                    registerUserRequestModel.mobile = CountryCode+"-"+ Number;
                                else
                                    registerUserRequestModel.mobile = "+27-" + Number;
                            }
                            else
                            {
                                registerUserRequestModel.mobile = "string";
                            }
                            registerUserRequestModel.eMail = email;
                            registerUserRequestModel.password = hash;
                            registerUserRequestModel.username = email;
                            registerUserRequestModel.appid = System.Guid.NewGuid().ToString(); ;
                            registerUserRequestModel.deviceid = System.Guid.NewGuid().ToString();
                            registerUserRequestModel.sysDate = DateTime.Now;
                            registerUserRequestModel.isVerifyEmail = false;
                            registerUserRequestModel.model = DeviceInfo.Model;
                            registerUserRequestModel.dType = DeviceInfo.DeviceType.ToString();
                            registerUserRequestModel.oVersion = DeviceInfo.Version.ToString();
                            registerUserRequestModel.dPlatform = DeviceInfo.Platform.ToString();
                            registerUserRequestModel.didiom = DeviceInfo.Idiom.ToString();
                            registerUserRequestModel.customerId = "";
                            registerUserRequestModel.genderId = gen.id;
                            registerUserRequestModel.dateOfBirth = dobirth;
                            if (CountryId != 0)
                                registerUserRequestModel.countryId = CountryId;
                            var result = await BreathtechAPIManager.Register(registerUserRequestModel);
                            UserDialogs.Instance.HideLoading();
                            if (result != null && result.issuccess == true)
                            {
                                await DisplayAlert("Success", result.outMSG, "Continue");
                                await Navigation.PopAsync();
                            }
                            else if (result != null && result.issuccess == false)
                            {
                                //await Navigation.PushAsync(new MaintenanceAlert());
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
                //ErrorMsg.Text = "Error :" + ex;
                await DisplayAlert("Error", "Something went wrong", "Cancel");
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void TermsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LegalDocumentTemplate("Terms Of Use", "Terms.html",""));
        }

        [Obsolete]
        private async void GoogleLogin_Clicked(object sender, EventArgs e)
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
                userLoginDelegate = async (object sender1, GoogleClientResultEventArgs<GoogleUser> e1) =>
                {
                    switch (e1.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e1.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e1.Data.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = e1.Data.Picture.AbsoluteUri,
                                Name = e1.Data.Name,
                                Email = e1.Data.Email
                            };
                            GoogleLoginRequestModel googleLoginRequestModel = new GoogleLoginRequestModel();
                            googleLoginRequestModel.GoogleId = socialLoginData.Id;
                            googleLoginRequestModel.UserName = socialLoginData.Name;
                            googleLoginRequestModel.Email = socialLoginData.Email;
                            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                            if (isInternetConnectionEnabled)
                            {
                                var result = await BreathtechAPIManager.GoogleLogin(googleLoginRequestModel);
                                if (result != null && result.isSuccess == true)
                                {
                                    Preferences.Set("customer_id", result.userDetails.customerId);
                                    Preferences.Set("recordid", result.userDetails.recordID.ToString());
                                    Preferences.Set("auth_token", "Bearer " + result.token);
                                    Preferences.Set("Email", result.userDetails.email);
                                    Preferences.Set("SubscriptionExpiryDate", result.userDetails.subscriptionEndDate.ToString("dd/MM/yyyy"));
                                    Preferences.Set("SubscriptionActive", result.userDetails.isSubcriptionActive.ToString());
                                    await App.Current.MainPage.Navigation.PushAsync(new Intro());
                                }
                                else if (result != null && result.isSuccess == false)
                                {
                                    await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                                    //await Navigation.PushAsync(new MaintenanceAlert());
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
                //Debug.WriteLine(ex.ToString());
                //await App.Current.MainPage.DisplayAlert("Error", "Google Login Cancelled", "Cancel");
            }
        }

        //[Obsolete]
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



        //                osmosysRestAPI.INSBTUGUsers(usr.GivenName, usr.FamilyName, "NA", usr.Email, usr.Id, usr.Email, "NA", "NA", usr.VerifiedEmail,
        //                   "NA", "NA", "NA", "NA", "NA", 2);

        //                var name = "";

        //                if (usr.GivenName != "NA")
        //                {
        //                    name = usr.GivenName;
        //                }
        //                else
        //                {
        //                    name = "User";
        //                }

        //                osmosysRestAPI.SendMail("info@breathtech.co.za", usr.Email, "Registration Confirmation", "Hi " + name + ",\nYour registration with" +
        //                    " breathtech has been successful. Thank you for your support. If you have any enquiries regarding technical support, " +
        //                    "please contact help@breathtech.co.za \n\n" + "Regards, \n" + "BreathTech Team");
        //                await DisplayAlert("Registration Successful", "Confirmation Email is Being Sent will be sent to you shortly", "continue");
        //                await SecureStorage.SetAsync("oauth_token", usr.Id);
        //                await Navigation.PushAsync(new MainPage());
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

        public async void Country()
        {
          
            //RegisterUserRequestModel registerUserRequestModel = new RegisterUserRequestModel();

            //ListView countryPicker = new ListView();
            //countryPicker.ItemsSource = registerUserRequestModel.countryId.ToString();


        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            this.cview_countryPopup.IsVisible = true;
        }

        //---------- Code for Getting Country List for profile uodate----------------------

        private UcCountry selectedCountry;

        public UcCountry SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;
                if (value != null)
                {
                    var country = selectedCountry as UcCountry;
                    this.CountryCode = country.countryCode;
                    this.CountryFlag = country.countryFlag;
                    this.CountryId = country.countryId;
                    /* if (registration != null)
                     {
                         registration.CountryCode = country.countryCode;
                         registration.CountryFlag = country.countryFlag;
                         registration.CountryId = country.countryId;
                     }
                     else
                     {
                         this.CountryCode = country.countryCode;
                         this.CountryFlag = country.countryFlag;
                         this.CountryId = country.countryId;
                     }*/
                    //UserRegisterationVM.SelectedCountry = country.countryCode;
                    //UserRegisterationVM.SelectedCountryCode = country.countryPrefix;
                    //UserRegisterationVM.SelectedCountryImage = country.countryFlag;
                    //UserRegisterationVM.CountryId = country.countryId;
                    //PopupNavigation.PopAsync();
                    this.cview_countryPopup.IsVisible = false;
                    //App.Current.MainPage.Navigation.PushAsync(new UserRegistrationTemplate(country));
                }
                OnPropertyChanged("SelectedCountry");
            }
        }
        private string searchCountry;
        public string SearchCountry
        {
            get
            {
                return searchCountry;
            }
            set
            {
                searchCountry = value;
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    GetAllCountriesPopup();
                }
                else
                {
                    try
                    {
                        CountryList = new List<UcCountry>(CountryListCopied.Where(s => s.countryName.ToLower().Contains(value.ToLower())));
                    }
                    catch (Exception ex) { }
                }
                OnPropertyChanged("SearchCountry");
            }
        }

        private List<UcCountry> countryList;

        public List<UcCountry> CountryList
        {
            get
            {
                return countryList;
            }
            set
            {
                countryList = value;
                OnPropertyChanged("CountryList");
            }
        }

        private List<UcCountry> countryListCopied;
        public List<UcCountry> CountryListCopied
        {
            get
            {
                return countryListCopied;
            }
            set
            {
                countryListCopied = value;
                OnPropertyChanged("CountryListCopied");
            }
        }

        public async void GetAllCountriesPopup()
        {
            CountryList = new List<UcCountry>();
            try
            {
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    var result = await BreathtechAPIManager.GetAllCountryData();
                    if (result != null && result.issuccess == true)
                    {
                        CountryList = result.btCountry.ToList();
                        CountryListCopied = CountryList;
                    }
                    else if (result != null && result.issuccess == false)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
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

            }
            catch (Exception ex)
            {

            }
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class UserDetails
    {
        public int recordID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mobile { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }
        public string appid { get; set; }
        public string deviceid { get; set; }
        public DateTime sysDate { get; set; }
        public bool isVerifyEmail { get; set; }
        public string model { get; set; }
        public string dType { get; set; }
        public string oVersion { get; set; }
        public string dPlatform { get; set; }
        public string didiom { get; set; }
        public string customerId { get; set; }
        public int countryId { get; set; }
    }



}