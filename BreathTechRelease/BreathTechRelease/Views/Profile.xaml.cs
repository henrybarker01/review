using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.RequestModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Authentication;
using BreathTechRelease.Models;
using Newtonsoft.Json;
using System.Net.Http;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Essentials;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Manager;
using BreathTechRelease.Service;
using Acr.UserDialogs;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : Xamarin.Forms.ContentPage
    {
        public string email, icon;
        GetUserDetailByIdResponseModel userDetail;

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


        public Profile()
        {
            InitializeComponent();

            //FName.Detail = "Test";
            //LName.Detail = "Test";
            //Mobile.Detail = "MobileTest";
            //Email.Detail = "www.email.com";
            CountryFlag = "za.png";
            CountryCode = "+27";
            CountryId = 314;
            GetData();
            BindingContext = this;
        }


        private async void GetData()
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
                        CountryId = data.userData.countryId;

                        var CountryList = new List<UcCountry>();
                        var result = await BreathtechAPIManager.GetAllCountryData();
                        if (result != null && result.issuccess == true)
                        {
                            CountryList = result.btCountry.ToList();
                            var countryData = CountryList.Where(s => s.countryId == CountryId).FirstOrDefault();
                            if (countryData != null)
                            {
                                CountryFlag = countryData.countryFlag;
                                CountryCode = countryData.countryCode;
                            }
                        }
                        else if (result != null && result.issuccess == false)
                        {
                            await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                        }
                        //await GetAllCountries();
                        FName.Text = data.userData.fname.ToString();
                        LName.Text = data.userData.lname.ToString();
                        Email.Text = data.userData.email.ToString();
                        dobText.Text = data.userData.dateOfBirth.ToString();
                        genderText.Text = data.userData.genderName.ToString();
                        if (!string.IsNullOrEmpty(data.userData.mobile) && !data.userData.mobile.Contains("string") && !string.IsNullOrEmpty(CountryCode))
                        {
                            string[] mobileNo = data.userData.mobile.Split('-');
                            CountryCode = mobileNo[0];
                            Mobile.Text = mobileNo[1];
                        }
                        if (!string.IsNullOrEmpty(data.userData.genderName))
                        {
                            genderText.Text = data.userData.genderName;
                        }
                        if (!string.IsNullOrEmpty(data.userData.dateOfBirth.ToString()))
                        {
                            dobText.Text = data.userData.dateOfBirth.ToString("dd/MM/yyyy");
                        }

                        //if (string.IsNullOrEmpty(data.userData.dateOfBirth.ToString()))
                        //{

                        //    dob.IsVisible = true;
                        //    dateText.IsVisible = false;
                        //}
                        //else
                        //{

                        //    genderText.Text = data.userData.genderName;
                        //    dob.IsVisible = false;
                        //    dateText.IsVisible = true;
                        //}
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

        public async Task GetAllCountries()
        {
            var CountryList = new List<UcCountry>();
            try
            {
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    var result = await BreathtechAPIManager.GetAllCountryData();
                    if (result != null && result.issuccess == true)
                    {
                        CountryList = result.btCountry.ToList();
                        var countryData = CountryList.Where(s => s.countryId == CountryId).FirstOrDefault();
                        CountryFlag = countryData.countryFlag;
                        CountryCode = countryData.countryCode;
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

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void ProfilePicture_Tapped(object sender, EventArgs e)
        {
            //Icon_Profile_Picture.png




        }
        private async void ChangePassword_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ChangePassword());
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfileView());
            //var gen = gender.SelectedItem as LstGender;
            //var dobirth = dob.Date;

            //if (string.IsNullOrEmpty(FName.Text))
            //{
            //    ErrorMsg.Text = "First Name cannot be empty";
            //}
            //else if (string.IsNullOrEmpty(LName.Text))
            //{
            //    ErrorMsg.Text = "Last Name cannot be empty";
            //}
            //else if (string.IsNullOrEmpty(Mobile.Text))
            //{
            //    ErrorMsg.Text = "Mobile Number cannot be empty";
            //}
            //else
            //{
            //    var auth_token = Preferences.Get("auth_token", "");
            //    string customerid = Preferences.Get("customer_id", "");
            //    UpdateProfileRequestModel updateProfileRequestModel = new UpdateProfileRequestModel();
            //    updateProfileRequestModel.fname = FName.Text;
            //    updateProfileRequestModel.lname = LName.Text;
            //    string recordId = Preferences.Get("recordid", "");
            //    updateProfileRequestModel.recordID = Convert.ToInt32(recordId);
            //    updateProfileRequestModel.mobile = Mobile.Text;
            //    updateProfileRequestModel.eMail = Email.Text;

            //    updateProfileRequestModel.genderId = gen.id;
            //    updateProfileRequestModel.dateOfBirth = dobirth;
            //    updateProfileRequestModel.appid = "";
            //    updateProfileRequestModel.deviceid = "";
            //    updateProfileRequestModel.sysDate = DateTime.Now;
            //    updateProfileRequestModel.oVersion = "";
            //    updateProfileRequestModel.didiom = "";
            //    updateProfileRequestModel.countryId = 0;
            //    updateProfileRequestModel.customerId = customerid;
            //    updateProfileRequestModel.deviceid = "";
            //    updateProfileRequestModel.didiom = "";
            //    updateProfileRequestModel.dPlatform = "";
            //    updateProfileRequestModel.dType = "";
            //    updateProfileRequestModel.isVerifyEmail = false;
            //    updateProfileRequestModel.username = "";
            //    updateProfileRequestModel.model = "";
            //    updateProfileRequestModel.password = "";
            //    var result = await BreathtechAPIManager.UpdateProfile(auth_token,updateProfileRequestModel);
            //    if (result != null && result.issuccess == true)
            //    {

            //        await DisplayAlert("Success", result.outMSG, "Cancel");

            //        //await Navigation.PushAsync(new SubscriptionListView());
            //    }
            //    else if (result != null && result.issuccess == false)
            //    {
            //        await DisplayAlert("Error", result.outMSG, "Cancel");
            //    }
            //    else
            //    {
            //        await Navigation.PushAsync(new MaintenanceAlert());
            //    }
        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class UserData
    {
        public int recordID { get; set; }
        public object uid { get; set; }
        public object fname { get; set; }
        public object lname { get; set; }
        public object username { get; set; }
        public object email { get; set; }
        public object customerId { get; set; }
        public bool isSubcriptionActive { get; set; }
        public DateTime subscriptionEndDate { get; set; }
        public int roleID { get; set; }
        public int subscriptionTypeId { get; set; }
        public object subscriptionType { get; set; }
        public object mobile { get; set; }
        public int countryId { get; set; }
        public object countryFlag { get; set; }
    }

    public class Root
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public UserData userData { get; set; }
    }

}
