using System;
using BreathTechRelease.Service;
using Xamarin.Forms;

namespace BreathTechRelease.Constant
{
    public class StringConstant
    {
        public static string kTermsKey = "kTermsKey";
        public static string kPrivacyKey = "kPrivacyKey";

        public static async void PermissionToOpenSetting()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                var answer = await App.Current.MainPage.DisplayAlert("Permission",
                    "It looks like your privacy settings are preventing us from accessing your camera." +
                    "Do you want to navigate to settings to provide permission?",
                    "Ok", "Cancel");
                if (answer)
                    DependencyService.Get<IMediaPermission>().AllowCameraPermission();
            }
        }
    }
}
