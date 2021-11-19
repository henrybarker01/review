using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.MenuItems;
using Xamarin.Essentials;
using BreathTechRelease.Views;
using Acr.UserDialogs;

namespace BreathTechRelease.Views
{
    public partial class MainPage : MasterDetailPage
    {
        string CoreMenuChoice = "";
        public DateTime currentdate = DateTime.Now;
        public List<MasterPageItem> MenuList { get; set; }
        public MainPage(Boolean IsWelcome)
        {
            try
            {
                InitializeComponent();
                SetMenuListData(IsWelcome);
            }
            catch (Exception ex)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public void SetMenuListData(Boolean IsWelcome)
        {
            MenuList = new List<MasterPageItem>();

            //Icon and menu item title

            var page0 = new MasterPageItem() { Title = "HOME", Icon = "Icon_Home.png", TargetType = typeof(MainView) };
            var page1 = new MasterPageItem() { Title = "INTRODUCTION", Icon = "Icon_Tutorial.png", TargetType = typeof(Intro) };
            var page2 = new MasterPageItem() { Title = "CORNERSTONES", Icon = "Icon_Cornerstones.png", TargetType = typeof(CoreMenuTemplate) };
            var page3 = new MasterPageItem() { Title = "BASIC BREATHING", Icon = "Icon_BasicBreathing.png", TargetType = typeof(CoreMenuTemplate) };
            var page4 = new MasterPageItem() { Title = "EVERYDAY BREATHING", Icon = "Icon_EverydayBreathing.png", TargetType = typeof(CoreMenuTemplate) };
            var page5 = new MasterPageItem() { Title = "FIND YOUR PATH", Icon = "Icon_FindyourPath.png", TargetType = typeof(SubMenuListViewTemplate) };
            var page6 = new MasterPageItem() { Title = "ABOUT US", Icon = "AboutUs.png", TargetType = typeof(LegalDocumentTemplate) };
            var page7 = new MasterPageItem() { Title = "NEWS", Icon = "Icon_News.png", TargetType = typeof(NewsView) };
            var page8 = new MasterPageItem() { Title = "MY ACCOUNT", Icon = "MyProfile.png", TargetType = typeof(Account) };
            //var page9 = new MasterPageItem() { Title = "Subscription", Icon = "Icon_Tutorial.png", TargetType = typeof(SubscriptionListView) };
            var page9 = new MasterPageItem() { Title = "SUBSCRIPTION", Icon = "Subscribe.png", TargetType = typeof(Subscribe) };
            var page10 = new MasterPageItem() { Title = "HOW TO NAVIGATE THE APP", Icon = "Icon_News.png", TargetType = typeof(NavigationLayout) };
            var page11 = new MasterPageItem() { Title = "LOGOUT", Icon = "logout.png", TargetType = typeof(logout) };
            // var page12 = new MasterPageItem() { Title = "NavigationLayout", Icon = "Icon_MyAccount.png", TargetType = typeof(NavigationLayout) };

            //Add menu Items to menuList
            MenuList.Add(page0);
            MenuList.Add(page1);
            MenuList.Add(page10);
            MenuList.Add(page2);
            MenuList.Add(page3);
            MenuList.Add(page4);
            MenuList.Add(page5);
            MenuList.Add(page9);
            MenuList.Add(page6);
            MenuList.Add(page7);
            MenuList.Add(page8);

            //MenuList.Add(page11);

            MenuList.Add(page11);
            //  MenuList.Add(page12);

            //Set List to be ItemSource for ListView in MainPage.xaml

            NavigationDrawerList.ItemsSource = MenuList;

            //Initial naviagtion, This is our home Page
            if (IsWelcome)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Welcome)));
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainView)));
            }

            //Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainView)));

            this.BindingContext = new
            {
                Header = "",
                Image = "",
                Footer = "© BreathTech 2021"
            };
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (MasterPageItem)e.SelectedItem;
                Type page = item.TargetType;
                if (item.Title == "EVERYDAY BREATHING")
                {
                    if(App.SubscriptionType=="Free")
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                    }
                    else if (currentdate>=App.SubscriptionEndDate)
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                        /*if (Preferences.Get("subscriptionExpired", false) == true || Preferences.Get("subscriptionType", "") == "Free")
                        {
                            //Detail = new NavigationPage((Page)Activator.CreateInstance(page, "EVERYDAY BREATHING"));
                            await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                            await Navigation.PushAsync(new Subscribe());
                        }*/
                    }
                    else
                    {
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, "EVERYDAY BREATHING"));

                    }

                }
                else if (item.Title == "CORNERSTONES")
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page, "CORNERSTONES"));
                }
                else if (item.Title == "FIND YOUR PATH")
                {
                    if (App.SubscriptionType == "Free")
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                    }
                    else if (currentdate>=App.SubscriptionEndDate)
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                        /*if (Preferences.Get("subscriptionExpired", false) == true || Preferences.Get("subscriptionType", "") == "Free")
                        {
                            //Detail = new NavigationPage((Page)Activator.CreateInstance(page, "FIND YOUR PATH"));
                            await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                            await Navigation.PushAsync(new Subscribe());
                        }*/
                    }
                    else
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, "FIND YOUR PATH"));
                }
                else if (item.Title == "NEWS")
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                    //await DisplayAlert("Notice", "News content not available at the moment", "Ok");
                    //if (Preferences.Get("subscriptionExpired", false) == true || Preferences.Get("subscriptionType", "") == "Free")
                    //{
                    //    await DisplayAlert("Notice", "News content not available at the moment", "Ok");

                    //}
                    //else
                    //    Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                }
                else if (item.Title == "BASIC BREATHING")
                {
                    if (App.SubscriptionType == "Free")
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                    }
                    else if (currentdate>=App.SubscriptionEndDate)
                    {
                        await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                        await Navigation.PushAsync(new Subscribe());
                        /*if (Preferences.Get("subscriptionExpired", false) == true || Preferences.Get("subscriptionType", "") == "Free")
                        {
                            //Detail = new NavigationPage((Page)Activator.CreateInstance(page, "BASIC SKILLS"));
                            await DisplayAlert("Alert", "Subscription expired/not valid, please subscribe to access all the premium content", "Ok");
                            await Navigation.PushAsync(new Subscribe());
                        }*/
                    }
                    else
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, "BASIC BREATHING"));
                }
                else if (item.Title == "ABOUT US")
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page, "About Us", "AboutUs.html", ""));
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                }

                IsPresented = false;
                NavigationDrawerList.SelectedItem = null;
                if (Navigation.NavigationStack.Count == 2)
                {
                    //   App.Current.MainPage.Navigation.RemovePage(Navigation.NavigationStack.());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override bool OnBackButtonPressed()
        {
            var mainpage = new MainPage(false);
            bool result = true;
            if (App.Current.MainPage == mainpage)
            {
                result = false;
            }

            return result;
        }
    }
}