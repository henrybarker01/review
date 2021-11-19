using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.Models;
using OAuthNativeFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Support : ContentPage
    {
        public Support()
        {
            InitializeComponent();
            TbEmail.Text = Constants.SupportEmailId;
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            var email = TbEmail.Text;
            var name = TbName.Text;
            var feedback = TBFeedback.Text;
            if (string.IsNullOrEmpty(name))
            {
                ErrorMsg.Text = "User Name cannot be empty";
            }
            bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
            if (isInternetConnectionEnabled)
            {
                var auth_token = Preferences.Get("auth_token", "");
                UserDialogs.Instance.ShowLoading("Loading");
                //var result = await BreathtechAPIManager.SendToSupport(auth_token, "arjunwwindia@gmail.com", email, feedback, name);

                //UserDialogs.Instance.HideLoading();
                //if (result != null && result.isSuccess == true)
                //{
                //    await DisplayAlert("Email sent successfully", result.outMSG, "continue");
                //    Navigation.PopAsync();
                //}
                //else if(result != null && result.isSuccess==false)
                //{
                //    await DisplayAlert("Error", "Unable to send email", "continue");
                //}
                //else
                //{
                //    await Navigation.PushAsync(new MaintenanceAlert());
                //}
                
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
            }
        }
    }
}