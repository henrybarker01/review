using System;
using BreathTechRelease.Helpers;
using BreathTechRelease.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

[assembly: ExportRenderer(typeof(SuperListView), typeof(MyListViewRenderer))]
namespace BreathTechRelease.iOS.Renderers
{
    public class MyListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            var superListView = Element as SuperListView;
            //superListView.n
            if (superListView == null)
                return;


            Control.ScrollEnabled = superListView.IsScrollingEnable;
        }
    }
}
