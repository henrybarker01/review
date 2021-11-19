using System;
using BreathTechRelease.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace BreathTechRelease.iOS.Renderers
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var tabs = Element as TabbedPage;

            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                {

                    if (TabBar.Items[i] == null) continue;

                    if (TabBar.Items[i].Title.Length > 6)
                    {
                        string[] splitTitle = TabBar.Items[i].Title.Split(" ");
                        if(splitTitle.Length == 2)
                        {
                            TabBar.Items[i].Title = splitTitle[0] + "\n" + splitTitle[1];

                            UITabBarItem item = TabBar.Items[i] as UITabBarItem;
                            UIView view = item.ValueForKey(new Foundation.NSString("view")) as UIView;
                            
                            UILabel label = view.Subviews[0] as UILabel;
                            //label.Text = "Hello\nWorld!";
                            label.Lines = 2;
                            
                            label.LineBreakMode = UILineBreakMode.WordWrap;
                        }
                        if(splitTitle.Length == 4)
                        {
                            TabBar.Items[i].Title = splitTitle[0] + " " + splitTitle[1] + "\n" + splitTitle[2] + " " + splitTitle[3];

                            UITabBarItem item = TabBar.Items[i] as UITabBarItem;
                            UIView view = item.ValueForKey(new Foundation.NSString("view")) as UIView;
                            UILabel label = view.Subviews[0] as UILabel;
                            //label.Text = "Hello\nWorld!";
                            label.Lines = 2;
                            label.LineBreakMode = UILineBreakMode.WordWrap;
                        }
                        

                        //var frame = label.Frame;
                        //label.Frame = CGRect.FromLTRB(frame.Location.X, frame.Location.Y, frame.Size.Width, frame.Size.Height + 20);
                    }

                }
            }
        }

    }
}
