using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Models;
using System.Security.Cryptography.X509Certificates;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntemediaryView : Xamarin.Forms.ContentPage
    {

        public string URL_Text;
        public string URL_Media, URL_Aud, URL_Vid;
        public string icn, dsc;

        public IntemediaryView(string heading, string introtext, string icon, string name, string desc, string urltext, string aud, string vid)
        {
            InitializeComponent();



            Heading.Text = heading;
            IntroText.Text = introtext;
            Icon.Source = icon;
            //Name.Text = name;
            //Desc.Text = desc;
            URL_Text = urltext;

            URL_Media = "";
            URL_Aud = aud;
            URL_Vid = vid;
            icn = icon;
            dsc = desc;
            //txtHeadinding.Text = name;

        }

       
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            

        }

        //protected override bool OnBackButtonPressed()
        //{
        //    base.OnBackButtonPressed();
        //    return false;
        //}

        private async void Close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }

        private async void ReadMore_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentViewTemplate(URL_Text, Heading.Text));
        }

        private async void Audio_Tapped(object sender, EventArgs e)
        {
            URL_Media = URL_Aud;
            if(URL_Media=="")
            {
                await DisplayAlert("Alert", "There is no audio for this section", "okay");
            }
            else
            {
                await Navigation.PushAsync(new MediaElement(URL_Media, Heading.Text));
            }
            
        }

        private async void Video_Tapped(object sender, EventArgs e)
        {
            URL_Media = URL_Vid;
            if (string.IsNullOrEmpty(URL_Media))
            {
                await DisplayAlert("Alert", "There is no video for this section", "okay");
            }
            else
            {
                await Navigation.PushAsync(new MediaElement(URL_Media, Heading.Text));
            }
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private async void Search_Pressed(object sender, EventArgs e)
        {
            searchBar.IsVisible = true;
        }


    }
}