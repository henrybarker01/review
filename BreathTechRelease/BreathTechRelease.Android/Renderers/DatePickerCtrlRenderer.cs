using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BreathTechRelease.CustomControls;
using BreathTechRelease.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]

namespace BreathTechRelease.Droid.Renderers
{
    [Obsolete]
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
                /*var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(30f);
                gradientDrawable.SetStroke(2, Android.Graphics.Color.LightGray);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);*/
                DatePickerCtrl element = Element as DatePickerCtrl;
                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    //Control.Text = element.Placeholder;
                    Control.Text = string.Empty;
                    //Control.SetTextColor(Android.Graphics.Color.Gray);
                    Control.Hint = "  Select Date of birth";
                }
                this.Control.TextChanged += (sender, arg) => {
                    var selectedDate = arg.Text.ToString();
                    if (selectedDate == element.Placeholder)
                    {
                        Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                };

            }
        }
    }
}