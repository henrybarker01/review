﻿using System;
using Xamarin.Forms;

namespace BreathTechRelease.CustomControls
{
    public class CustomViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty = BindableProperty.Create
            ("SelectedItemBackgroundColor", typeof(Color), typeof(CustomViewCell),Color.White);

        public Color SelectedItemBackgroundColor
        {
            get
            {
                return (Color)GetValue(SelectedItemBackgroundColorProperty);
            }
            set
            {
                SetValue(SelectedItemBackgroundColorProperty, value);
            }
        }
    }
}
