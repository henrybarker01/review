﻿using System;
using Xamarin.Forms;

namespace BreathTechRelease.CustomControls
{
    public class MaterialFrame : Frame
    {
        public MaterialFrame()
        {
        }

        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(MaterialFrame), 4.0f);

        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }
    }
}
