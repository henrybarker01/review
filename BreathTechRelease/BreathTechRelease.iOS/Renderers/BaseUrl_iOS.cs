using System;
using BreathTechRelease.iOS.Renderers;
using BreathTechRelease.Service;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace BreathTechRelease.iOS.Renderers
{
    public class BaseUrl_iOS : IBaseUrl
    {        
            public string Get()
            {
                return NSBundle.MainBundle.BundlePath +"/Content/";
            }
        
    }
}
