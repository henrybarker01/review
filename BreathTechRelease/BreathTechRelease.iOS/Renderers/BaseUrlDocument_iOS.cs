using System;
using BreathTechRelease.Service;
using Xamarin.Forms;
using Foundation;
using BreathTechRelease.iOS.Renderers;

[assembly: Dependency(typeof(BaseUrlDocument_iOS))]
namespace BreathTechRelease.iOS.Renderers
{
    public class BaseUrlDocument_iOS : IBaseURLDocument
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath + "/Documents/";
        }
    }
}
