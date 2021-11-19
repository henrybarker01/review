using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BreathTechRelease.Views
{
    public partial class MaintenanceAlert : ContentPage
    {
        //public ICommand TapCommand
        //{
        //    get
        //    {
        //        return new Command<string>(OpenBrowser);
        //    }
        //}

        public MaintenanceAlert()
        {
            InitializeComponent();
            BindingContext = this;
        }



        //void OpenBrowser(string url)
        //{
        //    Device.OpenUri(new Uri(url));
        //}
    }
}
