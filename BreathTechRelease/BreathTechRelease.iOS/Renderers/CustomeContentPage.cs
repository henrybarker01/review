using System;
using BreathTechRelease.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomeContentPage))]
namespace BreathTechRelease.iOS.Renderers
{
    public class CustomeContentPage:PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            
        }
    }
}
