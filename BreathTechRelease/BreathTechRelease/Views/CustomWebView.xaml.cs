﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomWebView : WebView
    {
        public static readonly BindableProperty CustomHeaderValueProperty = BindableProperty.Create(
            propertyName: nameof(CustomHeaderValue),
            returnType: typeof(string),
            declaringType: typeof(CustomWebView),
            defaultValue: default(string)
        );

        public string CustomHeaderValue
        {
            get => (string)GetValue(CustomHeaderValueProperty);
            set => SetValue(CustomHeaderValueProperty, value);
        }

        public CustomWebView()
        {
            InitializeComponent();
        }
    }
}