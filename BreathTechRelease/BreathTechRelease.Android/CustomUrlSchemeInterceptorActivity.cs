using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;


namespace BreathTechRelease.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
          new[] { Intent.ActionView },
          Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
          DataSchemes = new[] { "com.googleusercontent.apps.404477101244-179ui03uo9o6d2f7dfuuu6grcpf1rcve" },
          DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Android.Net.Uri uri_android = Intent.Data;

            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            this.Finish();

            return;
        }
    }
}