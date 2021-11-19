using System;
using Android.Content;
using Android.Widget;
using AndroidX.AppCompat.Graphics.Drawable;
using BreathTechRelease.Droid.Renderers;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using ImageButton = Android.Widget.ImageButton;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(IconNavigationPageRenderer))]
namespace BreathTechRelease.Droid.Renderers
{
    public class IconNavigationPageRenderer: MasterDetailPageRenderer
    {
        public IconNavigationPageRenderer(Context context ):base(context)
        {
                 
        }
        private static AndroidX.AppCompat.Widget.Toolbar GetToolbar() => (CrossCurrentActivity.Current?.Activity as MainActivity)?.FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var toolbar = GetToolbar();
            if (toolbar != null)
            {
                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as ImageButton;

                    var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                    if (drawerArrow == null)
                        continue;

                    imageButton.SetImageDrawable(Forms.Context.GetDrawable(Resource.Drawable.Hamburgericon));
                }
            }
        }
    }
}
