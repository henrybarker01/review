using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentViewTemplate : ContentPage
    {
        public ContentViewTemplate(string url, string heading)
        {
            InitializeComponent();
            Heading.Text = heading;
            URL_Text.Source = NetworkService.htmlContentsUrl+"/"+ url;

            //DependencyService.Get<IBaseUrl>().Get() + url;
        }

        private void CLOSE_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
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