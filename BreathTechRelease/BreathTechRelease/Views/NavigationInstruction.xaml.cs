using System;
using System.Collections.Generic;
using BreathTechRelease.Service;
using Xamarin.Forms;

namespace BreathTechRelease.Views
{
    public partial class NavigationInstruction : ContentPage
    {
        public NavigationInstruction()
        {
            InitializeComponent();
            URL_Text.CustomHeaderValue = "About Us";
            Heading.Text = "How to Navigate";
            //URL_Text.Source= DependencyService.Get<IBaseUrl>
            URL_Text.Source = DependencyService.Get<IBaseURLDocument>().Get() + "AboutUs.html";
        }
    }
}   
