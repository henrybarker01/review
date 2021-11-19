using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : ContentPage
    {
        public ResetPassword()
        {
            InitializeComponent();
        }
        private async void btnResetPass_Clicked(object sender, EventArgs e)
        {
            var email = TbEmail.Text.ToLower();
            var emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //var Password = TbPassword.Text;
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    ErrorMsg.Text = "Email cannot be empty";
                }
                else
                {
                    if (Regex.IsMatch(email, emailPattern))
                    {
                        bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                        if (isInternetConnectionEnabled)
                        {
                            UserDialogs.Instance.ShowLoading("Loading");
                            var result = await BreathtechAPIManager.SendOTPMail(email);
                            UserDialogs.Instance.HideLoading();
                            if (result != null && result.isSuccess == true)
                            {
                                await DisplayAlert("Success", result.outMSG, "Continue");
                                App.isOtpSend = true;
                                App.otpEmail = email;
                                await Navigation.PushAsync(new ResetPasswordOTP(email));
                            }
                            else if(result !=null && result.isSuccess == false)
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
                    else
                    {
                        ErrorMsg.Text = "Invalid Email";
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