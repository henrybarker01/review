using Acr.UserDialogs;
using BreathTechRelease.Manager;
using BreathTechRelease.RequestModels;
using BreathTechRelease.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaElement : ContentPage
    {
        public string filName = "";
        public string mediaType = "";
        List<InsertUpdateMatriceRequestModel> list = new List<InsertUpdateMatriceRequestModel>();
        public MediaElement(string urlMedia, string heading)
        {
            InitializeComponent();
            cview_Media.IsVisible = true;
            activity.IsVisible = true;
            activity.IsRunning = true;
            activity.IsEnabled = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            InsertUpdateMatriceRequestModel insertUpdateMatriceRequestModel = new InsertUpdateMatriceRequestModel();
            string mediaString = urlMedia;
            string uid = Preferences.Get("uid", "");

            Heading.Text = heading;
            mediaType = mediaString;

            bool isInternetConnectionEnabled = checkNetworkConnectivity();
            if (isInternetConnectionEnabled)
            {
                string uri = NetworkService.mediaBaseUrl+"/"+ mediaString;
                Vid.Source = new Uri(uri);


                if (mediaType.ToLower().Contains("mp3"))
                {
                    filName = heading + " Audio";
                    insertUpdateMatriceRequestModel.isAudioVideo = true;
                }
                else if (mediaType.ToLower().Contains("mp4"))
                {
                    filName = heading + " Video";
                    insertUpdateMatriceRequestModel.isAudioVideo = false;
                }

                insertUpdateMatriceRequestModel.audioVideoName = mediaString;
                insertUpdateMatriceRequestModel.segmentName = heading;
                insertUpdateMatriceRequestModel.uid = uid;
                list.Add(insertUpdateMatriceRequestModel);
                SaveData(list);
            }
            else
            {
                cview_Media.IsVisible = false;
                activity.IsVisible = false;
                activity.IsRunning = false;
                activity.IsEnabled = false;
            }
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
                App.Current.MainPage.DisplayAlert("Alert", "Network is Not available. Please check your internet settings", "ok");
                return false;
            }
        }
        async void SaveData(List<InsertUpdateMatriceRequestModel> insertUpdateMatriceRequestModel)
        {
            try
            {

                string auth_token = Preferences.Get("auth_token", "");
                var result = BreathtechAPIManager.InsertUpdateMatricesBulk(auth_token, insertUpdateMatriceRequestModel);
            }
            catch (Exception ex)
            {

            }
        }

        async void OnMediaOpened(object sender, EventArgs e)
        {
            //await DisplayAlert("Now Opening", filName + ".", "ok");

            //Vid.KeepScreenOn = true;
            Vid.Play();
            cview_Media.IsVisible = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            activity.IsEnabled = false;
        }

        async void OnMediaFailed(object sender, EventArgs e)
        {
            cview_Media.IsVisible = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            activity.IsEnabled = false;
            await DisplayAlert("Problem Encountered", "Media failed.", "ok");
        }

        async void OnMediaEnded(object sender, EventArgs e)
        {
            Vid.Stop();

        }

        async void OnSeekCompleted(object sender, EventArgs e)
        {
        }

        private async void CLOSE_Clicked(object sender, EventArgs e)
        {

            //Vid.Pause();
            Vid.Stop();
            //Vid.IsEnabled = false;
            await Navigation.PopAsync();

        }



    }
}