using System;
using BreathTechRelease.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(PageiOS))]
namespace BreathTechRelease.iOS.Renderers
{
    public class PageiOS : PageRenderer
    {
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            nfloat tabSize = 44.0f;


            UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;

            if (UIInterfaceOrientation.LandscapeLeft == orientation || UIInterfaceOrientation.LandscapeRight == orientation)
            {
                tabSize = 34.0f;
            }

            CGRect rect = this.View.Frame;
            // rect.Y = this.NavigationController != null ? tabSize : tabSize + 20;
            this.View.Frame = rect;

            if (TabBarController != null)
            {
                rect.Y = this.NavigationController != null ? tabSize : tabSize + 20;
                this.View.Frame = rect;
                CGRect tabFrame = this.TabBarController.TabBar.Frame;
                tabFrame.Height = tabSize;
                tabFrame.Y = 0;

                var txtfont = new UITextAttributes()
                {
                    Font = UIFont.SystemFontOfSize(25)
                };

                this.TabBarController.TabBar.Frame = tabFrame;
                this.TabBarController.TabBar.BarTintColor = UIColor.White;
                
                this.TabBarController.TabBar.TintColor = UIColor.White;


            }
        }
        private void UpdateItem(UITabBarItem item, UIColor selected, UIColor unselected)
        {
            if (item == null) return;

            item.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = selected
            }, UIControlState.Selected);

            item.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = unselected
            }, UIControlState.Normal);
        }
    }
}
