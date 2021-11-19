using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreathTechRelease.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using BreathTechRelease.Droid.Renderers;

[assembly: ExportRenderer(typeof(RoundedEntry),typeof(RoundedEntryRendererAndroid))]

namespace BreathTechRelease.Droid.Renderers
{
    public class RoundedEntryRendererAndroid : EntryRenderer
    {
        public RoundedEntryRendererAndroid(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}