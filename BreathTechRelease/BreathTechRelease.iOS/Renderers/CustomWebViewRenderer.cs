using System;
using BreathTechRelease.iOS.Renderers;
using BreathTechRelease.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace BreathTechRelease.iOS.Renderers
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            EventHandler loadHandler = (s, evtArgs) =>{

                var url = (s as UIWebView).Request.Url;

                System.Diagnostics.Debug.WriteLine("URl:"+ (s as UIWebView).Request.Url);

                if (url.ToString().Contains("Complete"))
                {
                    if(url.ToString().Contains("Complete/1"))
                        MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ClosePaymentScreen", "Success");
                    else
                        MessagingCenter.Send<App, string>((App)Xamarin.Forms.Application.Current, "ClosePaymentScreen", "Failed");
                }
            };

            var webView = Control as UIWebView;

            if (webView == null)
            {
                webView = new UIWebView();
                webView.ScalesPageToFit = true;
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                webView.LoadFinished -= loadHandler;
            }

            if (e.NewElement != null)
            {
                webView.LoadFinished += loadHandler;

                var headerKey = new NSString("Authorization");
                var headerValue = new NSString(Element.CustomHeaderValue);
                var dictionary = new NSDictionary(headerKey, headerValue);

                UrlWebViewSource source = (Xamarin.Forms.UrlWebViewSource)Element.Source;
                var webRequest = new NSMutableUrlRequest(new NSUrl(source.Url));
                webRequest.Headers = dictionary;

                Control.LoadRequest(webRequest);
            }

        }
    }
}