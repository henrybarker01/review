using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Models;
using Xamarin.Essentials;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class SubMenuListViewTemplate : ContentPage
    {
        public string icon, name, desc;
        List<ListViewTemplate> myList;
        private async void Close_Clicked(object sender, EventArgs e)
        {
            var x = Navigation.NavigationStack.Count;
            if (x == 2)
                await Navigation.PopAsync(true);
            else
                await Navigation.PushAsync(new MainPage(false));
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text != null)
            {
                var searchResult = myList.Where(c => c.Name.Contains(searchBar.Text.ToUpper()));
                ItemListView.ItemsSource = searchResult;
            }
            else
            {
                ItemListView.ItemsSource = myList;
            }
        }

        public SubMenuListViewTemplate(string heading)
        {
            InitializeComponent();

            Heading.Text = heading;
            string type = Preferences.Get("subscriptionType", "");
            string isSubscriptionActive = Preferences.Get("SubscriptionActive", "");
            /*if (type == "Free" || isSubscriptionActive == "0")
                //searchImage.IsVisible = false;
                searchImage.Source = "";*/

            List<ListViewTemplate> listFindYourPath = new List<ListViewTemplate>
            {
             new ListViewTemplate
             {
                 Icon="FindYourPath_Bodywise.png",
                 Name="BODYWISE",
                 Desc="",
                 Count=0,

             },

             new ListViewTemplate
             {
                 Icon="FindYourPath_HeartIntelligence.png",
                 Name="HEART INTELLIGENCE",
                 Desc="",
                 Count=0,

             },

               new ListViewTemplate
             {
                 Icon="FindYourPath_BusinessEdge.png",
                 Name="BUSINESS EDGE",
                 Desc="",
                 Count=0,

             },
           
              new ListViewTemplate
             {
                 Icon="FindYourPath_GoforGold.png",
                 Name="GO FOR GOLD",
                 Desc="",
                 Count=0,

             },

               new ListViewTemplate
             {
                 Icon="FindYourPath_BeYourBest.png",
                 Name="BE YOUR BEST",
                 Desc="",
                 Count=0,

             },

                new ListViewTemplate
             {
                 Icon="FindYourPath_MakingMagic.png",
                 Name="MAKING MAGIC",
                 Desc="",
                 Count=0,

             },

                 new ListViewTemplate
             {
                 Icon="FindYourPath_HelpingtheHelper.png",
                 Name="HELPING THE HELPER",
                 Desc="",
                 Count=0,

             },

                  new ListViewTemplate
             {
                 Icon="FindYourPath_SoulSource.png",
                 Name="SOUL SOURCE",
                 Desc="",
                 Count=0,
             },

            };
            ItemListView.ItemsSource = listFindYourPath;
            ItemListView.HeightRequest = listFindYourPath.Count * 200;
            myList = listFindYourPath;

        }

        private async void ItemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as ListViewTemplate;

            var In = new ListViewTemplate();

            switch (selected.Icon)
            {
                //Find Your Path

                case "FindYourPath_Bodywise.png":
                    icon = "FindYourPath_Bodywise.png";
                    desc = "BODYWISE";
                    break;

                case "FindYourPath_BusinessEdge.png":
                    icon = "FindYourPath_BusinessEdge.png";
                    desc = "BUSINESS EDGE";
                    break;


                case "FindYourPath_HeartIntelligence.png":
                    icon = "FindYourPath_HeartIntelligence.png";
                    desc = "HEART INTELLIGENCE";
                    break;

                case "FindYourPath_GoforGold.png":
                    icon = "FindYourPath_GoforGold.png";
                    desc = "GO FOR GOLD";
                    break;
                case "FindYourPath_BeYourBest.png":
                    icon = "FindYourPath_BeYourBest.png";
                    desc = "BE YOUR BEST";
                    break;

                case "FindYourPath_MakingMagic.png":
                    icon = "FindYourPath_MakingMagic.png";
                    desc = "MAKING MAGIC";
                    break;
                case "FindYourPath_HelpingtheHelper.png":
                    icon = "FindYourPath_HelpingtheHelper.png";
                    desc = "HELPING THE HELPER";
                    break;

                case "FindYourPath_SoulSource.png":
                    icon = "FindYourPath_SoulSource.png";
                    desc = "SOUL SOURCE";
                    break;

            }

            await Navigation.PushAsync(new SubMenuTemplate(icon, desc));

        }

        private async void Search_Pressed(object sender, EventArgs e)
        {
            searchBar.IsVisible = true;
            //search.IsVisible = false;
            //await PopupNavigation.PushAsync(new SearchPopup());
        }
    }
}