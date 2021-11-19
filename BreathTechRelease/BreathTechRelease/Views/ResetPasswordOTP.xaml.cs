using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordOTP : ContentPage
    {
        private string email;
        public ResetPasswordOTP(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        private async void btnVerifyOTP_Clicked(object sender, EventArgs e)
        {
            var Otp = TbOTP.Text;
            try
            {
                if(string.IsNullOrEmpty(Otp))
                {
                    ErrorMsg.Text = "Please Enter OTP";
                }
                else
                {
                    bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                    if (isInternetConnectionEnabled)
                    {
                        UserDialogs.Instance.ShowLoading("Loading");
                        var result = await BreathtechAPIManager.ValidateOTP(email, Otp);
                        UserDialogs.Instance.HideLoading();
                        if (result != null && result.isSuccess == true)
                        {
                            await DisplayAlert("Success", result.outMSG, "Continue");
                            await Navigation.PushAsync(new ResetEnterPasswordPage(email));
                        }
                        else if(result != null && result.isSuccess == false)
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
            }

            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //ErrorMsg.Text = "Error:" + ex;
                await DisplayAlert("Error", "Something went wrong", "Cancel");
            }
        }
    }
}