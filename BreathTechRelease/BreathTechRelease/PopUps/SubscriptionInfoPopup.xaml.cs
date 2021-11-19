using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscriptionInfoPopup : PopupPage
    {
        public DataSource data = new DataSource();
        public SubscriptionInfoPopup(string Name = "", string Desc = "", string Price = "")
        {
            InitializeComponent();
            //data.Name = Name;
            //data.Desc = Desc;
            //data.Price = Price;

            //BindingContext = data;
            lblTitle.Text = Name;
            lblDesc.Text = Desc;
            lblPrice.Text = Price;

        }
    }

    public class DataSource {
        public string Name;
        public string Desc;
        public string Price;
    }
}
