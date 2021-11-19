using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Models;

using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using OAuthNativeFlow;
using Xamarin.Essentials;
using BreathTechRelease.RequestModels;
using BreathTechRelease.ResponseModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LegalDocumentTemplate : ContentPage
    {
        string docName = "";
        GetUserDetailByIdResponseModel userDetail;
        public List<ListViewTemplate> myList;
        public LegalDocumentTemplate(string docName1, string docPath, string docSupport)
        {
            InitializeComponent();
            docName = docName1;
            if (docName == "Support")
            {
                GetData();
            }
            Heading.Text = docName1.ToUpper();
            URL_Text.CustomHeaderValue = docName1;
            TBEmail.Text = Constants.SupportEmailId;
            GetHTMLView();
            
            /*URL_Text.On<Android>().EnableZoomControls(true);
            URL_Text.On<Android>().DisplayZoomControls(true);*/
            //URL_Text.Source =   DependencyService.Get<IBaseURLDocument>().Get() + docPath;
            
            
            // this.Navigation = navigation;
        }
        //private async void CLOSE_Clicked(object sender, EventArgs e)
        //{
        //    //Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync(true));
        //    await Navigation.PopAsync(true);
        //}
        private async void Close_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage(false));
            int Pagecount = Navigation.NavigationStack.Count;
            if (Pagecount > 1)
                await Navigation.PopAsync(true);
            else
                App.Current.MainPage = new MainPage(false);
        }
        private async void GetHTMLView()
        {
            try
            {
                await Task.Run(() =>
                {
                    ListViewTemplate listView = new ListViewTemplate();

                    List<ListViewTemplate> listTeam = new List<ListViewTemplate>
                {
                 new ListViewTemplate
                 {
                     BioName="Dorian Cabral",
                     BioImage ="Dorion_About.png",
                     BioText = "Dorian Cabral is a successful entrepreneur and sought after business advisor. He is the owner of IT Anywhere Pty (Ltd), Once Active GYM and Membr Management Software for Southern Africa. It took one private session of breathwork with Dan Brulé to inspire Dorian to create, mastermind and drive the development of the BreathTech App."

                 },
                  new ListViewTemplate
                 {
                     BioName="Dr Ela Manga",
                     BioImage="DrManga_About.png",
                     BioText="Dr Ela Manga is an Integrative Medical Doctor, author, speaker and leading voice in the field of mind-body medicine and wellness.\n\n"+
                             "Her background in western medicine and study of wisdom traditions have informed her unique approach to health and wellbeing.\n\n"+
                             "She is an expert in the field of energy management and burnout. Her first book BREATHE: Strategising Energy in the Age of Burnout, is fast becoming the go to guide for managing energy and optimising physical and mental health.\n\n"+
                             "Ela is a catalyst for healing in the lives of individuals, groups and broader society and provides a compassionate but challenging space for her clients in their journey to wholeness.\n\n"+
                             "Ela is a sought-after speaker, both locally and internationally, and has a revolutionary way of facilitating groups for profound transformation.\n\n"+
                             "Her unique focus on breathwork supports personal transformation and change across many communities and sectors. She is the founder of Breathwork Africa, a social enterprise that offers training and support to breathwork practitioners across the African Continent.\n\n"+
                             "She lives and works in the vibrant city of Johannesburg in South Africa.",
                 },
                 new ListViewTemplate
                 {
                     BioName="Dan Brulé",
                     BioImage="Dan_About.png",
                     BioText="Dan Brulé is a modern-day teacher, healer, and world-renown pioneer in the Art and Science of Breathwork. He is the founder of the International Center for Breathwork and The Breathing Festival.  Dan is one of the creators of Breath Therapy and he was among the original group of Internationally Certified Rebirthers. He is a master of Prana Yoga (the Hindu Science of Breath) and Chi Kung/Qigong (Chinese Medical Breathing Exercises), and he leads the worldwide Spiritual Breathing Movement. He is known as the Bruce Lee of Breathwork, The Godfather of Breathing, and the Poet Laureate of the Breath!\n\n"+
                             "Dan coaches, trains, and certifies Professional Breathworkers, and since 1970, he has travelled to 67 countries and has trained more than 250,000 people to use conscious breathing for personal healing and growth, professional development, peak performance, and spiritual awakening. He has trained Olympic athletes, military special forces and elite martial artists. He coaches leading medical experts, psychotherapists, yoga instructors, meditators, fitness gurus, corporate executives, life coaches, and celebrities including Tony Robbins.\n\n"+
                             "Dan is the author of several books including “Just Breathe: Mastering Breathwork for Success in Life, Love, Business and Beyond.”(Published by Simon & Schuster. Foreword by Tony Robbins. Now in 10 languages).",
                 }
                };
                    repeaterView.ItemsSource = listTeam;
                    myList = listTeam;

                    if (docName == "Support")
                    {
                        URL_Text.Source = "https://payments.breathtechapp.com/Pages/Support.html";
                        DocLayout.IsVisible = false;
                        EmailLayout.IsVisible = true;
                        BioLayout.IsVisible = false;
                        SupportLayout.IsVisible = true;

                    }
                    else if (docName == "About Us")
                    {
                        URL_Text.Source = "https://payments.breathtechapp.com/pages/PrivacyPolicy.html";
                        DocLayout.IsVisible = false;
                        BioLayout.IsVisible = true;
                        EmailLayout.IsVisible = false;
                        SupportLayout.IsVisible = false;
                        DocLayout.HeightRequest = 1750;

                    }
                    else if (docName == "Terms Of Use")
                    {
                        URL_Text.Source = "https://breathtechapp.com/terms-and-conditions/";
                        DocLayout.IsVisible = true;
                        BioLayout.IsVisible = false;
                        SupportLayout.IsVisible = false;
                        EmailLayout.IsVisible = false;
                    }
                    else if (docName == "Privacy Policy")
                    {
                        URL_Text.Source = "https://breathtechapp.com/privacy-policy/";
                        DocLayout.IsVisible = true;
                        BioLayout.IsVisible = false;
                        EmailLayout.IsVisible = false;
                        SupportLayout.IsVisible = false;
                    }
                });
            }
            catch (Exception ex)
            {
                ToastConfig toastConfig = new ToastConfig(ex.Message);
                toastConfig.SetDuration(2000);
                toastConfig.SetBackgroundColor(Color.Gray);
                UserDialogs.Instance.Toast(toastConfig);
            }
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchBar.Text))
            {
                var searchResult = myList.Where(x => x.BioName.Contains(searchBar.Text));
                repeaterView.ItemsSource = searchResult.ToList();
            }
            else
            {
                repeaterView.ItemsSource = myList;
            }
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

                        var CountryList = new List<UcCountry>();
                        var result = await BreathtechAPIManager.GetAllCountryData();
                        if (result != null && result.issuccess == true)
                        {

                        }
                        else if (result != null && result.issuccess == false)
                        {
                            await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                        }
                        TbName.Text = data.userData.fname;
                        TbLastName.Text = data.userData.lname!=null?data.userData.lname:"";
                        //flagImage.Source = data.userData.countryFlag != null ? data.userData.countryFlag : "za.png";
                        flagImage.Source = (!string.IsNullOrEmpty(data.userData.countryFlag)) ? data.userData.countryFlag : "za.png";
                        //lblCountryCode.Text = data.userData.countryId != 0 ? data.userData.countryId.ToString() : "+27-";
                        if (!string.IsNullOrEmpty(data.userData.mobile))
                        {
                            string[] mobileno = data.userData.mobile.Split('-');
                            if (mobileno.Length == 3)
                            {
                                lblCountryCode.Text = mobileno[0] + mobileno[1]+"-";
                                TbContactno.Text = mobileno[2];
                            }
                            else
                            {
                                lblCountryCode.Text = mobileno[0] + "-";
                                TbContactno.Text = mobileno[1];
                            }
                        }
                        else
                        {
                            lblCountryCode.Text = "+27-";
                            TbContactno.Text = "";
                        }
                        //TbContactno.Text = data.userData.mobile != null ? data.userData.mobile : "";
                        TBEmail.Text = data.userData.email.ToString();
                        UserDialogs.Instance.HideLoading();
                        TBFeedback.IsEnabled = true;
                        TBFeedback.Focus();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Alert", "Network is Not available. Please check your internet settings", "ok");
                }
            }
            catch
            {

            }
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            //var email = TBEmail.Text;
            var name = TbName.Text;
            var lName = TbLastName.Text;
            var mobileNo = lblCountryCode + "-" + TbContactno.Text;
            var feedback = TBFeedback.Text;
            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    ErrorMsg.Text = "Name cannot be empty";
                }

                else if (string.IsNullOrEmpty(feedback))
                {
                    ErrorMsg.Text = "Feedback cannot be empty";
                }
                else if (isInternetConnectionEnabled)
                {
                    var auth_token = Preferences.Get("auth_token", "");
                    UserDialogs.Instance.ShowLoading("Loading");
                    string userEmail = Preferences.Get("Email", "");
                    SupportHistoryRequestModel supportHistoryRequestModel = new SupportHistoryRequestModel();
                    supportHistoryRequestModel.name = name;
                    supportHistoryRequestModel.lName = lName!=null?lName:"";
                    supportHistoryRequestModel.userEmail = userEmail;
                    supportHistoryRequestModel.toEmail = userEmail;
                    supportHistoryRequestModel.contactNo = mobileNo!=null?mobileNo:"";
                    supportHistoryRequestModel.message = feedback;
                    var result = await BreathtechAPIManager.SendToSupport(auth_token, Constants.SupportEmailId, supportHistoryRequestModel);
                    UserDialogs.Instance.HideLoading();
                    if (result != null && result.isSuccess == true)
                    {
                        await DisplayAlert("Email sent successfully", result.outMSG, "continue");
                        Navigation.PopAsync();
                    }
                    else if (result != null && result.isSuccess == false)
                    {
                        await DisplayAlert("Error", "Unable to send email", "continue");
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
            catch (Exception ex)
            {

            }
        }
        private async void Search_Pressed(object sender, EventArgs e)
        {
            searchBar.IsVisible = true;
            //search.IsVisible = false;
            //await PopupNavigation.PushAsync(new SearchPopup());
        }
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        void URL_Text_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            UserDialogs.Instance.HideLoading();
        }

        void URL_Text_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
        }
    }
}