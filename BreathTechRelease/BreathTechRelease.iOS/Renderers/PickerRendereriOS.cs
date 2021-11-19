using BreathTechRelease.CustomControls;
using BreathTechRelease.iOS.Renderers;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(PickerCtrl), typeof(PickerRendereriOS))]
namespace BreathTechRelease.iOS.Renderers
{
    public class PickerRendereriOS : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(Control!=null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                //Control.Layer.CornerRadius = 10;
                //Control.Layer.BorderWidth = 0.5f;
                //Control.Layer.BorderColor = UIColor.LightGray.CGColor;
                Control.TextColor = Color.LightGray.ToUIColor();
            }
           

        }
    }
}