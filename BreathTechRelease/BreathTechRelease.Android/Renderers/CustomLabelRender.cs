using System;
using Android.Content;
using Android.Graphics.Text;
using Android.Text;
using BreathTechRelease.CustomControls;
using BreathTechRelease.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using JustificationMode = Android.Text.JustificationMode;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRender))]
namespace BreathTechRelease.Droid.Renderers
{
    public class CustomLabelRender:LabelRenderer
    {
        public CustomLabelRender(Context context) : base(context)
        {

        }
        protected CustomLabel LetterSpacingLabel { get; private set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.JustificationMode = JustificationMode.InterWord;
                this.LetterSpacingLabel = (CustomLabel)this.Element;
            }
            var letterSpacing = this.LetterSpacingLabel.LetterSpacing;
            this.Control.LetterSpacing = letterSpacing;
            this.UpdateLayout();
        }
    }
}
