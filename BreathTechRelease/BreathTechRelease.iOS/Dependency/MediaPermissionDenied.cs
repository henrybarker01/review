using System;
using BreathTechRelease.iOS.Dependency;
using BreathTechRelease.Service;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaPermissionDenied))]
namespace BreathTechRelease.iOS.Dependency
{
    public class MediaPermissionDenied : IMediaPermission
    {
        public MediaPermissionDenied()
        {
        }

        static MediaPermissionDenied pathManager;
        public static MediaPermissionDenied Instance
        {
            get
            {
                if (pathManager == null)
                    pathManager = new MediaPermissionDenied();
                return pathManager;
            }
        }

        public void AllowCameraPermission()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
        }
    }
}
