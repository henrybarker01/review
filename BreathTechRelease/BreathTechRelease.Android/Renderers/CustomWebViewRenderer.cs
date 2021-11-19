using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using BreathTechRelease.Droid.Renderers;
using BreathTechRelease.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace BreathTechRelease.Droid.Renderers
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, Android.Webkit.WebView>
    {
        Android.Content.Context _localContext;
        public CustomWebViewRenderer(Context context) : base(context)
        {
            _localContext = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            Android.Webkit.WebView webView = Control as Android.Webkit.WebView;

            if (Control == null)
            {
                webView = new Android.Webkit.WebView(_localContext);
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                // ...
            }

            if (e.NewElement != null)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    ["Authorization"] = Element.CustomHeaderValue
                };

                webView.Settings.JavaScriptEnabled = true;

                webView.Settings.BuiltInZoomControls = true;
                webView.Settings.SetSupportZoom(true);

                webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
                webView.ScrollbarFadingEnabled = false;

                webView.SetWebViewClient(new CustomWebViewClient(headers));
                UrlWebViewSource source = Element.Source as UrlWebViewSource;

                //source.Url = @"http://154.0.166.250/BreathTechAPI/paygate/Indextest";
                try
                {
                    webView.LoadUrl(source.Url, headers);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public class CustomWebViewClient : Android.Webkit.WebViewClient
    {
        public Dictionary<string, string> headers { get; set; }

        public CustomWebViewClient(Dictionary<string, string> requestHeaders)
        {
            headers = requestHeaders;
        }

        public override void OnPageStarted(Android.Webkit.WebView view, string url, Android.Graphics.Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);
            System.Diagnostics.Debug.WriteLine("Loading website...");
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            System.Diagnostics.Debug.WriteLine("Load finished.");

            if (url.Contains("Complete"))
            {
                if (url.Contains("Complete/1"))
                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ClosePaymentScreen", "Success");
                else
                    MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ClosePaymentScreen", "Failed");
            }
        }

        public override void OnReceivedError(Android.Webkit.WebView view, IWebResourceRequest request, WebResourceError error)
        {
            base.OnReceivedError(view, request, error);
        }
    }
}