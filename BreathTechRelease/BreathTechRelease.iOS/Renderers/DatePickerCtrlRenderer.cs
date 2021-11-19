using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using BreathTechRelease.iOS.Renderers;
using UIKit;
using BreathTechRelease.CustomControls;
using CoreGraphics;
using Xamarin.Essentials;

[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace BreathTechRelease.iOS.Renderers
{
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
                return;
            var element = e.NewElement as DatePickerCtrl;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            //Control.BorderStyle = UITextBorderStyle.RoundedRect;
            //var blueHex = ColorConverters.FromHex("#3498db");

            //Control.Layer.BorderColor = UIColor.LightGray.CGColor;
            //Control.Layer.CornerRadius = 10;
            //Control.Layer.BorderWidth = 0.5f;
            //Control.AdjustsFontSizeToFitWidth = true;
            Control.TextColor = Color.LightGray.ToUIColor();   
                    
           Control.ShouldEndEditing += (textField) => {
               var seletedDate = (UITextField)textField;
               var text = seletedDate.Text;
               if (text == element.Placeholder)
               {
                   Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
               }
               return true;
           };
        }
        private void OnCanceled(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
        private void OnDone(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
    }
}
