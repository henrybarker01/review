using Acr.UserDialogs;
using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
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
    public partial class ResendVerificationMail : ContentPage
    {
        public ResendVerificationMail()
        {
            InitializeComponent();
        }
        private async void btnVerify_Clicked(object sender, EventArgs e)
        {
            var email = TbEmail.Text;
            try
            {
                var emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                if (email == null || email == "")
                {
                    ErrorMsg.Text = "Email cannot be empty";
                }
                else
                {
                    if(Regex.IsMatch(email, emailPattern))
                    {
                        bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                        if (isInternetConnectionEnabled)
                        {
                            UserDialogs.Instance.ShowLoading("Loading");
                            var result = await BreathtechAPIManager.ResendVerificationMail(email);
                            UserDialogs.Instance.HideLoading();
                            if (result != null && result.issuccess)
                            {   
                                await App.Current.MainPage.DisplayAlert("Alert", "Mail sent successfully", "OK");
                                App.Current.MainPage.Navigation.PopAsync();
                            }
                            else if(result != null && result.issuccess == false)
                            {
                                await DisplayAlert("Error", result.outMSG, "Cancel");
                                //await Navigation.PushAsync(new MaintenanceAlert());
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