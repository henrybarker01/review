using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using BreathTechRelease.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationLayout : ContentPage, INotifyPropertyChanged
    {
        public NavigationLayout()
        {
            InitializeComponent();
            this.BindingContext = this;
            Task.Run(() =>
            {
                cview_Intro.IsVisible = true;
                activity.IsVisible = true;
                activity.IsRunning = true;
                GetNavList();
            });

        }
        private async void Close_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainView());
            await Navigation.PushAsync(new CoreMenuTemplate("CORNERSTONES"));
        }

        private Timer timer;

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5000).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            cview_Intro.IsVisible = false;

            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (cvWalkthrough.Position == 2)
                {

                    cvWalkthrough.Position = 0;
                    return;
                }
                cvWalkthrough.Position += 1;
            });
        }
        private List<NavigationWalkthrough> _navigationWalkthroughs { get; set; }
        public List<NavigationWalkthrough> navigationWalkthroughs
        {
            get { return _navigationWalkthroughs; }
            set { _navigationWalkthroughs = value; OnPropertyChanged(); }
        }
        private async void GetNavList()
        {
            cview_Intro.IsVisible = true;
            activity.IsVisible = true;
            activity.IsRunning = true;
            await Task.Run(() =>
            {
                List<NavigationWalkthrough> walkthrough = new List<NavigationWalkthrough>();
                walkthrough.Add(new NavigationWalkthrough
                {
                    Caption = "BreathTech is your personal guide to enhancing your life through skillful conscious breathing.\n\n" +
                         "Our lifestyles, ways of eating, moving and living, have all impacted on the way we breathe and in turn," +
                         " on the way we think, feel, behave, and experience life.\n\n" +
                         "This app has been designed to help you to reset your breathing back to its natural optimal state,and so.\n\n" +
                         "We suggest that you start by mastering the three Cornerstones.\n\n" +
                         "The app helps you to become more conscious or mindful of your breathing. And it guides you in learning to use your breath to relax" +
                         " and release physical, emotional and psychological tension or negativity. And it shows you how to tap your breath as a source of pure genuine " +
                         "life-force energy.",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_1.png",
                    Height = 0
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "",
                    Caption = "Once you have practiced the Cornerstones, we suggest you move on to the series of “Basic Breathing” exercises.\n\n" +
                     "Then focus on “Breathing for Everyday Life:” practical techniques that you can use right away to feel better and to support and improve your everyday life activities.\n\n" +
                     "Once you have practiced and mastered these, you will be ready to focus on your chosen path.And from there, explore all 8 Paths to develop breathing mastery.",
                    Image = "NavigationB.png",
                    Image0 = "NavigationGFG.png",
                    Image1 = "NavigationHI.png",
                    Image2 = "NavigationBE.png",
                    Image3 = "NavigationBYB.png",
                    Image4 = "NavigationMM.png",
                    Image5 = "NavigationHTH.png",
                    Image6 = "NavigationSS.png",
                    BackgroundImage = "WhitePages_2.png",
                    Height = 400
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "",
                    Caption = "We encourage you to be playful and get creative as you craft your own unique breathing practice, but always returning to the basic foundations to fine-tune and deepen your skill sets.\n\n" +
                     "We are here to support you in fulfilling your heart’s greatest desire, and reaching your full potential!\n\n" +
                     "You can do it! You can be a biohacker! You can empower, uplift and inspire yourself, and others. You can change your life by changing how you breathe! \n\n" +
                     "Scroll through the next few slides for guidance on how to tackle insomnia, confidence, peak performance, business, depression, grief and anger.",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    Image7 = "NavigationBT.png",
                    BackgroundImage = "WhitePages_3.png",
                    Height = 0
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "INSOMNIA",
                    Caption = "Step 1: Start with the Cornerstones" +
                     "Step 2: Explore the Basic Breathing series" +
                     "Step 3: Find and practice the technique for Insomnia in the Bodywise path" +
                     "Step 4: Perhaps move to the techniques for emotional wellbeing" +
                     "Step 5: Then return to the Cornerstones and Basic Breathing" +
                     "Step 6: Start moving along your chosen pathway" +
                     "Step 7: Create your own personal journey by including exercises from the other paths"

  ,
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_4.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with practicing the Cornerstones",
                    Step2 = "Explore the Basic Breathing series",
                    Step3 = "Find and practice the technique for Insomnia in the Bodywise path",
                    Step4 = "Perhaps move to the techniques for emotional wellbeing",
                    Step5 = "Then return to the Cornerstones and Basic Breathing",
                    Step6 = "Start moving along your chosen pathway",
                    Step7 = "Create your own personal journey by including exercises from the other path",
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "CONFIDENCE",
                    Caption = "Step 1: Start with the Cornerstones\n" +
                     "Step 2: Practice the Basic Breathing series\n" +
                     "Step 3: Go to Be Your Best and practice all the techniques\n" +
                     "Step 4: Find a technique under Go For Gold, Business Edge and Heart Intelligence\n" +
                     "Step 5: Then move back to the Cornerstones and Basic Breathing\n" +
                     "Step 6: Start moving along your chosen pathway\n" +
                     "Step 7: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path\n"

  ,
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_5.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with practicing the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Go to Be Your Best and practice all the techniques",
                    Step4 = "Find a technique under Go For Gold, Business Edge and Heart Intelligence",
                    Step5 = "Then move back to the Cornerstones and Basic Breathing",
                    Step6 = "Start moving along your chosen pathway",
                    Step7 = "Create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "PEAK PERFORMANCE",
                    Caption = "Step 1: Start with practicing the Cornerstones" +
                     "Step 2: Practice the Basic Breathing series" +
                     "Step 3: Find the Go For Gold Path and practice each technique (perhaps one per week)" +
                     "Step 4: Move on to Be Your Best pathway and practice each techniques (perhaps one per week)" +
                     "Step 5: Then move back to the Cornerstones and Basic Breathing" +
                     "Step 6: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_6.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with practicing the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Find the Go For Gold Path and practice each technique (perhaps one per week)",
                    Step4 = "Move on to Be Your Best pathway and practice each techniques (perhaps one per week)",
                    Step5 = "Then move back to the Cornerstones and Basic Breathing",
                    Step6 = "Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "BUSINESS",
                    Caption = "Step 1: Start with the Cornerstones" +
                     "Step 2: Practice the Basic Breathing series" +
                     "Step 3: Go through all the techniques in the Business Edge pathway" +
                     "Step 4: Move on to Be your Best and Heart Intelligence" +
                     "Step 5: Then move back to cornerstones and basic breathing parameters" +
                     "Step 6: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_7.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Go through all the techniques in the Business Edge pathway",
                    Step4 = "Move on to Be your Best and Heart Intelligence",
                    Step5 = "Then move back to cornerstones and basic breathing parameters",
                    Step6 = "Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "DEPRESSION",
                    Caption = "Step 1: Start with the Cornerstones" +
                     "Step 2: Practice the Basic Breathing Parameters" +
                     "Step 3: Find and practice the technique for Depression in the Heart Intelligence pathway" +
                     "Step 4: Find a technique under Be your Best and Soul Source" +
                     "Step 5: Then move back to the Cornerstones and the series of Basic Breathing exercises" +
                     "Step 6: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_8.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Find and practice the technique for Depression in the Heart Intelligence pathway",
                    Step4 = "Find a technique under Be your Best and Soul Source",
                    Step5 = "Then move back to the Cornerstones and the series of Basic Breathing exercises",
                    Step6 = "Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "GRIEF",
                    Caption = "Step 1: Start with the Cornerstones\n" +
                     "Step 2: Practice the series of Basic Breathing exercises\n" +
                     "Step 3: Find and practice the technique for Grief under Heart Intelligence\n" +
                     "Step 4: Go to Soul Source and find a technique that you feel drawn to\n" +
                     "Step 5: Then move back to the Cornerstones and Basic Breathing series\n" +
                     "Step 6: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path\n",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_9.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Find and practice the technique for Grief under Heart Intelligence",
                    Step4 = "Go to Soul Source and find a technique that you feel drawn to",
                    Step5 = "Then move back to the Cornerstones and Basic Breathing",
                    Step6 = "Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "ANGER",
                    Caption = "Step 1: Start with the Cornerstones\n" +
                     "Step 2: Practice the Basic Breathing series\n" +
                     "Step 3: Find and practice the technique for Anger in the Heart Intelligence path\n" +
                     "Step 4: Move to the techniques under Be your Best\n" +
                     "Step 5: Then move back to the Cornerstones and Basic Breathing series\n" +
                     "Step 6: Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path\n",
                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    BackgroundImage = "WhitePages_10.png",
                    Height = 0,
                    isCaption = false,
                    isSteps = true,
                    Step1 = "Start with the Cornerstones",
                    Step2 = "Practice the Basic Breathing series",
                    Step3 = "Find and practice the technique for Anger in the Heart Intelligence path",
                    Step4 = "Move to the techniques under Be your Best",
                    Step5 = "Then move back to the Cornerstones and Basic Breathing",
                    Step6 = "Start moving along your chosen pathway, and create your own personal journey by including exercises from the other path"
                });
                walkthrough.Add(new NavigationWalkthrough
                {
                    Heading = "",
                    Caption = "As you move through the app, you will build up wide and powerful skillsets which will support you in every aspect of your life—guaranteed! \n",
                     

                    Image = "",
                    Image0 = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = "",
                    Image5 = "",
                    Image6 = "",
                    Image7 = "NavigationETJ.png",
                    Image8 = "NavigationCornerstones.png",
                    BackgroundImage = "WhitePages_11.png",

                    Caption1 = "know that we are here to support you at every step along the way!\n",
                    Height = 0
                });
                navigationWalkthroughs = walkthrough;
            });
            cvWalkthrough.IsVisible = true;
            cvWalkthrough.ItemsSource = navigationWalkthroughs;
            cview_Intro.IsVisible = false;
            activity.IsVisible = false;
            activity.IsRunning = false;
        }

        public class NavigationWalkthrough
        {
            public string Heading { get; set; }
            public string Caption { get; set; }
            public string Caption1 { get; set; }
            public string Image { get; set; }

            public string Image0 { get; set; }
            public string Image1 { get; set; }
            public string Image2 { get; set; }
            public string Image3 { get; set; }
            public string Image4 { get; set; }
            public string Image5 { get; set; }
            public string Image6 { get; set; }

            public string Image7 { get; set; }

            public string Image8 { get; set; }
            public string BackgroundImage { get; set; }
            public int Height { get; set; }
            public bool isSteps { get; set; }
            public bool isCaption { get; set; } = true;
            public string Step1 { get; set; }
            public string Step2 { get; set; }
            public string Step3 { get; set; }
            public string Step4 { get; set; }
            public string Step5 { get; set; }
            public string Step6 { get; set; }
            public string Step7 { get; set; }

        }
    }
}


//"\u2022  Body Wise(Physical Health)\n" +
//"\u2022 Heart Intelligence(Emotional Wellbeing)\n" +
//"\u2022 Be your Best(Personal Growth)\n" +
//"\u2022 Go For Gold(Sports Performance)\n" +
//"\u2022 Business Edge(Breathing at work)\n " +
//"\u2022 Making Magic(Breathing for Creativity)\n " +
//"\u2022 Help the Helper(Support for Helping Professions) \n" +
//"\u2022 Soul Source(Spirituality and Connection)\n\n" +