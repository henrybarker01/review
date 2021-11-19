using System;
using BreathTechRelease.Droid.Renderers;
using BreathTechRelease.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace BreathTechRelease.Droid.Renderers
{
    public class BaseUrl_Android : IBaseUrl
    {
            public string Get()
            {
                return "file:///android_asset/Content/";
            }
    }
}
