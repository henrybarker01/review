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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        private async void btnChangePass_Clicked(object sender, EventArgs e)
        {
            var email = Preferences.Get("Email", "");
            var currentPassword = TbCurrentPassword.Text;
            var newPassword = TbNewPass.Text;
            var confirmPass = TbConfirmPass.Text;
            try
            {
                if (string.IsNullOrEmpty(currentPassword))
                {
                    ErrorMsg.Text = "Current Password cannot be empty";
                }
                else if (string.IsNullOrEmpty(newPassword))
                {
                    ErrorMsg.Text = "New Password cannot be empty";
                }
                else if (string.IsNullOrEmpty(confirmPass))
                {
                    ErrorMsg.Text = "Confirm Password cannot be empty";
                }
                else if (newPassword.Length <= 3)
                {
                    ErrorMsg.Text = "New Password Length Must be greater than 3 characters";
                }
                else if (!newPassword.Equals(confirmPass))
                {
                    ErrorMsg.Text = "Password and Confirm Password should be same";
                }
                else
                {
                    bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                    if (isInternetConnectionEnabled)
                    {
                        UserDialogs.Instance.ShowLoading("Loading");
                        // Create Hash value for password
                        string currentPasshash = "";
                        string newPasshash = "";
                        currentPasshash = Encryption.EncryptString(NetworkService.Key, currentPassword);
                        newPasshash = Encryption.EncryptString(NetworkService.Key, newPassword);
                        var auth_token = Preferences.Get("auth_token", "");
                        var result = await BreathtechAPIManager.ChangePassword(auth_token, email, currentPasshash, newPasshash);
                        UserDialogs.Instance.HideLoading();
                        if (result != null && result.isSuccess == true)
                        {
                            await DisplayAlert("Success", result.outMSG, "Continue");
                            Navigation.PopAsync();
                        }
                        else if (result != null && result.isSuccess == false)
                        {
                            await DisplayAlert("Error", result.outMSG, "Cancel");
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

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //ErrorMsg.Text = "Error :" + ex;
                await DisplayAlert("Error", "Something went wrong", "Cancel");
            }

        }
    }
}