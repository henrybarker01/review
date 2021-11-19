using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Helpers;
using BreathTechRelease.Manager;
using BreathTechRelease.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetEnterPasswordPage : ContentPage
    {
        private string email;
        public ResetEnterPasswordPage(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        private async void btnResetPass_Clicked(object sender, EventArgs e)
        {
            //var Otp = TbOTP.Text;
            var password = TbPassword.Text;
            var confirmPassword = TbCnfrmPassword.Text;
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    ErrorMsg.Text = "Password cannot be empty";
                }
                else if (password.Length <= 3)
                {
                    ErrorMsg.Text = "Password length must be greater than 3 characters";
                }
                else if (string.IsNullOrEmpty(confirmPassword))
                {
                    ErrorMsg.Text = "Confirm Password cannot be empty";
                }
                else if (!password.Equals(confirmPassword))
                {
                    ErrorMsg.Text = "Password and Confirm Password must be same";
                }
                else
                {
                    bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                    if (isInternetConnectionEnabled)
                    {
                        UserDialogs.Instance.ShowLoading("Loading");
                        // Create Hash value for password
                        //string hash = "";
                        string hash = Encryption.EncryptString(NetworkService.Key, password);
                        var result = await BreathtechAPIManager.ResetPassword(email, hash);
                        //var result = await BreathtechAPIManager.ResetPassword(email,Password);
                        UserDialogs.Instance.HideLoading();
                        if (result != null && result.isSuccess == true)
                        {
                            await DisplayAlert("Success", result.outMSG, "Continue");
                            App.isOtpSend = false;
                            App.otpEmail = string.Empty;
                            App.Current.MainPage = new NavigationPage(new Login());
                        }
                        else if (result != null && result.isSuccess == false)
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