using System;
using BreathTechRelease.Service;
using BreathTechRelease.Droid.Renderers;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrlDocument_Android))]
namespace BreathTechRelease.Droid.Renderers
{
    public class BaseUrlDocument_Android : IBaseURLDocument
    {
        public string Get()
        {
            return "file:///android_asset/Documents/";
        }
    }
}
