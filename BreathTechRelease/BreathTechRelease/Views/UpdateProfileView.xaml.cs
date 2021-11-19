using System;
using System.Collections.Generic;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.RequestModels;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Service;
using Xamarin.Essentials;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using BreathTechRelease.PopUps;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Media.Abstractions;
using System.IO;
using BreathTechRelease.Constant;
using Rg.Plugins.Popup.Extensions;
using BreathTechRelease.ViewModels;
using System.ComponentModel;

namespace BreathTechRelease.Views
{
    public partial class UpdateProfileView : ContentPage,INotifyPropertyChanged
    {
        public bool FromLoginScreen;

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

        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private ImageSource personImage = "placeholderRectImg.png";
        public ImageSource PersonImage
        {
            get
            {
                return personImage;
            }
            set
            {
                personImage = value;
                OnPropertyChanged("PersonImage");
            }
        }

        private Stream stream1;
        private string image;

        public UpdateProfileView(bool FromLoginScreen = false)
        {
            InitializeComponent();
            CountryFlag = "za.png";
            CountryCode = "+27";
            CountryId = 314;
            GetData();
            this.FromLoginScreen=FromLoginScreen;
            BindingContext=this;
            GetAllCountriesPopup();
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
                        CountryId = data.userData.countryId;

                        var CountryList = new List<UcCountry>();
                        var result1 = await BreathtechAPIManager.GetAllCountryData();
                        if (result1 != null && result1.issuccess == true)
                        {
                            CountryList = result1.btCountry.ToList();
                            var countryData = CountryList.Where(s => s.countryId == CountryId).FirstOrDefault();
                            if(countryData!=null)
                            {
                                CountryFlag = countryData.countryFlag;
                                CountryCode = countryData.countryCode;
                            }
                        }
                        else if (result1 != null && result1.issuccess == false)
                        {
                            await App.Current.MainPage.DisplayAlert("Error", result1.message, "Cancel");
                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                        }

                        //await GetAllCountries();
                        FName.Text = data.userData.fname.ToString();
                        LName.Text = data.userData.lname.ToString();
                        Email.Text = data.userData.email.ToString();
                        if (!string.IsNullOrEmpty(data.userData.mobile) && !data.userData.mobile.Contains("string") && !string.IsNullOrEmpty(CountryCode))
                        {
                            string[] mobileNo = data.userData.mobile.Split('-');
                            CountryCode = mobileNo[0];
                            Mobile.Text = mobileNo[1];
                        }
                        var result = await BreathtechAPIManager.GetAllGender();
                        if (result != null)
                        {
                            gender.ItemsSource = result.lstGender;
                        }
                        gender.SelectedIndex = data.userData.genderId - 1;
                        dob.Date = data.userData.dateOfBirth;
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
                    await App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            GetAllCountriesPopup();
            this.cview_countryPopup.IsVisible = true;
            CountryListView.ItemsSource = CountryListPop;
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new UpdateProfileView());
            UserDialogs.Instance.ShowLoading();
            var gen = gender.SelectedItem as LstGender;

            var dobirth = dob.Date;

            if (string.IsNullOrEmpty(FName.Text))
            {
                ErrorMsg.Text = "First Name cannot be empty";
            }
            else if (string.IsNullOrEmpty(LName.Text))
            {
                ErrorMsg.Text = "Last Name cannot be empty";
            }
            //else if (string.IsNullOrEmpty(Mobile.Text))
            //{
            //    ErrorMsg.Text = "Mobile Number cannot be empty";
            //}
            else if(gen==null)
            {
                ErrorMsg.Text = "Please select gender";
            }
            else
                {
                var auth_token = Preferences.Get("auth_token", "");
                string customerid = Preferences.Get("customer_id", "");
                UpdateProfileRequestModel updateProfileRequestModel = new UpdateProfileRequestModel();
                updateProfileRequestModel.fname = FName.Text;
                updateProfileRequestModel.lname = LName.Text;
                string recordId = Preferences.Get("recordid", "");
                updateProfileRequestModel.recordID = Convert.ToInt32(recordId);
                if (string.IsNullOrEmpty(Mobile.Text) || Mobile.Text == "")
                    updateProfileRequestModel.mobile = "string";
                else
                    updateProfileRequestModel.mobile =CountryCode+"-"+ Mobile.Text;
                updateProfileRequestModel.eMail = Email.Text;
                updateProfileRequestModel.genderId = gen.id;
                updateProfileRequestModel.dateOfBirth = dobirth;
                updateProfileRequestModel.appid = new Guid();
                updateProfileRequestModel.deviceid = "string";
                updateProfileRequestModel.sysDate = DateTime.Now;
                updateProfileRequestModel.oVersion = "string";
                updateProfileRequestModel.didiom = "string";
                updateProfileRequestModel.countryId = CountryId;
                updateProfileRequestModel.customerId = customerid;
                updateProfileRequestModel.deviceid = "string";
                updateProfileRequestModel.didiom = "string";
                updateProfileRequestModel.dPlatform = "string";
                updateProfileRequestModel.dType = "string";
                updateProfileRequestModel.isVerifyEmail = false;
                updateProfileRequestModel.username = "string";
                updateProfileRequestModel.model = "string";
                updateProfileRequestModel.password = "string";
                var result = await BreathtechAPIManager.UpdateProfile(auth_token, updateProfileRequestModel);
                if (result != null && result.issuccess == true)
                {
                    //await DisplayAlert("Success", result.outMSG, "Continue");

                    /*ToastConfig toastConfig = new ToastConfig("Profile updated successfully.");
                    toastConfig.SetDuration(3000);
                    toastConfig.SetBackgroundColor(Color.Gray);
                    UserDialogs.Instance.Toast(toastConfig);*/
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Success", "Profile updated successfully.", "Ok");
                    await Navigation.PushAsync(new Account());
                   /* if (FromLoginScreen)
                    {
                        App.Current.MainPage = new NavigationPage(new Intro());
                    }  
                    else
                    {
                        await App.Current.MainPage.Navigation.PopAsync();
                    }*/
                //await Navigation.PushAsync(new Profile());
                }
                else if (result != null && result.issuccess == false)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", result.outMSG, "Cancel");
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await Navigation.PushAsync(new MaintenanceAlert());
                }
               
            }
        }

       

        private void UpdateImage(ImageSource _image)
        {
            if (_image != null)
                //profilePic.Source = _image;

            if (stream1 != null)
            {
                byte[] filebytearray = new byte[stream1.Length];
                stream1.Read(filebytearray, 0, (int)stream1.Length);
                Console.WriteLine("Filename", stream1);
                image = Convert.ToBase64String(filebytearray);
                //UpdateProfileApiCall();
            }
        }

        Plugin.Media.Abstractions.MediaFile file1;
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                //trackProg.Add("Checking permission for picking photo from edit profile : ", "Yes");
                var action = await App.Current.MainPage.DisplayActionSheet("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");
                var galleryStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                var mediaStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.MediaLibrary);
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (action == "Choose Existing")
                {
                    try
                    {
                        //trackProg.Add("Inside Choose Existing : ", "Yes");
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                            return;
                        }
                        Plugin.Media.Abstractions.MediaFile file = null;
                        try
                        {
                            if (Device.RuntimePlatform == Device.Android)
                            {
                                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                                if (cameraStatus == Plugin.Permissions.Abstractions.PermissionStatus.Denied)
                                {
                                    file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                                    {
                                        Directory = "Test",
                                        SaveToAlbum = true,
                                        CompressionQuality = 75,
                                        CustomPhotoSize = 50,
                                        PhotoSize = PhotoSize.MaxWidthHeight,
                                        MaxWidthHeight = 2000,
                                        DefaultCamera = CameraDevice.Front
                                    });
                                }
                            }
                            else
                            {
                                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                                {
                                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                                    RotateImage = false,
                                    SaveMetaData = false,
                                });
                            }
                            //trackProg.Add("Image picked form gallery : ", "Yes");
                        }
                        catch (Exception ex)
                        {
                            //Crashes.TrackError(ex, trackProg);
                        }

                        if (file == null)
                            return;

                        if (file.Path != null)
                        {
                            Image = ImageSource.FromFile(file.Path);
                            stream1 = file.GetStream();
                            UpdateImage(Image);
                            file1 = file;
                            file.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Crashes.TrackError(ex, trackProg);
                    }

                }
                else if (action == "Take Photo")
                {
                    try
                    {
                        // trackProg.Add("Take Photo from camera from edit profile : ", "Yes");
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {

                            await CrossMedia.Current.Initialize();
                            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                            if (cameraStatus == Plugin.Permissions.Abstractions.PermissionStatus.Denied)
                            {
                                StringConstant.PermissionToOpenSetting();
                            }
                            Plugin.Media.Abstractions.MediaFile file = null;

                            try
                            {
                                if (cameraStatus == Plugin.Permissions.Abstractions.PermissionStatus.Denied)
                                {
                                    if (Device.RuntimePlatform == Device.Android)
                                    {
                                        file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                                        {
                                            Directory = "Test",
                                            SaveToAlbum = true,
                                            CompressionQuality = 75,
                                            CustomPhotoSize = 50,
                                            MaxWidthHeight = 2000,
                                            PhotoSize = PhotoSize.MaxWidthHeight,
                                            DefaultCamera = CameraDevice.Front
                                        });
                                    }
                                    else
                                    {
                                        file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                                        {
                                            Directory = "Test",
                                            SaveToAlbum = true,
                                            CompressionQuality = 75,
                                            CustomPhotoSize = 50,
                                            PhotoSize = PhotoSize.MaxWidthHeight,
                                            MaxWidthHeight = 2000,
                                            RotateImage = false,
                                            SaveMetaData = false,
                                            DefaultCamera = CameraDevice.Front
                                        });
                                    }
                                }
                                //trackProg.Add("Photo from camera from edit profle: ", "Yes");
                            }
                            catch (Exception ex)
                            {
                                //Crashes.TrackError(ex, trackProg);
                            }
                            if (file == null)
                                return;

                            if (file.Path != null)
                            {
                                Image = ImageSource.FromFile(file.Path);
                                stream1 = file.GetStream();
                                UpdateImage(Image);
                                file1 = file;
                                file.Dispose();
                            }

                        });
                    }
                    catch (Exception ex)
                    {
                        //Crashes.TrackError(ex, trackProg);
                    }
                }

            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex, trackProg);
            }
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
                        CountryListPop = new List<UcCountry>(CountryListCopied.Where(s => s.countryName.ToLower().Contains(value.ToLower())));
                    }
                    catch (Exception ex) { }
                }
                OnPropertyChanged("SearchCountry");
            }
        }

        private List<UcCountry> countryListPop;

        public List<UcCountry> CountryListPop
        {
            get
            {
                return countryListPop;
            }
            set
            {
                countryListPop = value;
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

        bool countryActivity = false;
        public async void GetAllCountriesPopup()
        {
            List<UcCountry>CountryList1 = new List<UcCountry>();
            try
            {
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    await Task.Run(async() =>
                    {
                        var result = await BreathtechAPIManager.GetAllCountryData();
                        if (result != null && result.issuccess == true)
                        {
                            CountryList1 = result.btCountry.ToList();
                        }
                        else if (result != null && result.issuccess == false)
                        {
                            //await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                        }
                        else
                        {
                            //await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                        }
                    });
                    CountryListPop = CountryList1;
                    CountryListCopied = CountryList1;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
