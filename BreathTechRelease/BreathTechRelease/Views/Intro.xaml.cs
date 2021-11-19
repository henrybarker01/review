using BreathTechRelease.Manager;
using BreathTechRelease.RequestModels;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Service;
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
    public partial class Intro : ContentPage
    {
        public string filName = "App Intro.mp4";
        public Intro()
        {
            //Task.Delay(100);
            InitializeComponent();
            skipButton.IsEnabled = false;
            cview_Intro.IsVisible = true;
            activity.IsVisible = true;
            activity.IsRunning = true;
            activity.IsEnabled = true;
            Vid.AutoPlay = false;

            string uri = "http://165.73.80.34/testmedia/" + filName;
            Vid.Source = new Uri(uri);
            //Vid.Source = "http://165.73.80.34/testmedia/" + filName;

            var apiService = NetworkService.GetApiService();

            var auth_token = Preferences.Get("auth_token", "");

            //StoreVideoData();

        }


        public async void StoreVideoData()
        {
            VideoPlayedRequestModel videoPlayedRequestModel = new VideoPlayedRequestModel();
            string recordId = Preferences.Get("recordid", "");

            videoPlayedRequestModel.isVideoPlayed = true;
            videoPlayedRequestModel.recordID = Int32.Parse(recordId);

            VideoPlayedResponseModel videPlayedResponseModel = new VideoPlayedResponseModel();



            var result = await BreathtechAPIManager.VideoPlayed(videoPlayedRequestModel.recordID, videoPlayedRequestModel.isVideoPlayed);

            if (result != null && result.isSuccess == true)
            {
                await DisplayAlert("", "Stored succesfully", "ok");
            }
            else
            {
                //await DisplayAlert("", "Error, cannot store video flag at this moment", "ok");
            }

        }
        async void OnMediaOpened(object sender, EventArgs e)
        {

            Vid.Play();
            Vid.BufferingProgress.ToString();
            skipButton.IsEnabled = true;
            cview_Intro.IsVisible = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
            activity.IsEnabled = false;
            StoreVideoData();
            //Small delay
            // await Task.Delay(100);

            //while(skipButton.IsEnabled == false)
            //{
            //    var duration = Vid.Position;
            //    if(duration.TotalMilliseconds>1)
            //    {
            //        skipButton.IsEnabled = true;
            //    }
            //}
        }

        async void OnMediaFailed(object sender, EventArgs e)
        {
            await DisplayAlert("Problem Encountered", "Media failed.", "ok");
        }

        async void OnMediaEnded(object sender, EventArgs e)
        {
            Vid.Stop();

        }

        async void OnSeekCompleted(object sender, EventArgs e)
        {
            skipButton.IsEnabled = true;
        }

        private async void Skip_Clicked(object sender, EventArgs e)
        {
            Vid.AutoPlay = false;
            Vid.Stop();



            //Navigation.RemovePage(this); 

           Application.Current.MainPage = new NavigationPage(new NavigationLayout());
            //await Navigation.PushAsync(new NavigationLayout());


            // await Navigation.PushAsync(new MainPage());
            //Navigation.RemovePage(this);

            //Navigation.InsertPageBefore(new Tutorial(), this);

            //await Navigation.PopAsync().ConfigureAwait(f salse);
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                if (Vid.Position != null)
                {
                    var duration = Vid.Position;

                    Task.Delay(100);
                    //Vid.Stop();
                    if (duration.TotalMilliseconds == 1)
                    {
                        string uri = "";
                        Vid.Source = new Uri(uri);
                    }
                    //else
                    //{
                    //    Vid.Stop();
                    //}
                    // Navigation.RemovePage(this);
                }

            }
            catch (Exception ex)
            {
                Vid.Stop();
            }
        }
    }
}