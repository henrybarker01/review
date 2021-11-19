using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.Templates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Models;
using BreathTechRelease.Service;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsView : ContentPage
    {
        public NewsView()
        {
            InitializeComponent();
            URL_Text.Source = NetworkService.newsBaseUrl;
        }
        private async void CLOSE_Clicked(object sender, EventArgs e)
        {
            var x = Navigation.NavigationStack.Count;
            if (x == 2)
                await Navigation.PopAsync(true);
            else
                await Navigation.PushAsync(new MainPage(false));
        }
        void URL_Text_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            cview_contentText.IsVisible = true;
            activity.IsVisible = true;
            activity.IsEnabled = true;
            activity.IsRunning = true;
        }

        void URL_Text_Navigated(System.Object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            cview_contentText.IsVisible = false;
            activity.IsVisible = false;
            activity.IsEnabled = false;
            activity.IsRunning = false;
        }
    }
}