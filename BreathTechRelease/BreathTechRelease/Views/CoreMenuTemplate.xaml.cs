using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreathTechRelease.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoreMenuTemplate : ContentPage
    {

        public string icon, name, desc, count;

        bool HasSMenu = false;

        public List<ListViewTemplate> myList;
        public CoreMenuTemplate(string cMenuChoice)
        {
            InitializeComponent();
            string type = Preferences.Get("subscriptionType", "");
            string isSubscriptionActive = Preferences.Get("SubscriptionActive", "");
            searchImage.IsVisible = true;
            if (type == "Free" || isSubscriptionActive == "0")
                //searchImage.IsVisible = false;
                //searchImage.ImageSource = "";
            ItemListView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
              ((ListView)sender).SelectedItem = null;
            };
            Heading.Text = cMenuChoice;

            switch (cMenuChoice)
            {

                case "CORNERSTONES":

                        Cornerstones();
                    break;

                case "EVERYDAY BREATHING":

                    EverydayBreathing();

                    break;
                    ;

                //case "Find Your Path":

                //    FindYourPath();

                //    break;
                //    ;

                case "BASIC BREATHING":

                    BasicBreathing();

                    break;
                    ;
            }


        }

        public async void Cornerstones()
        {
            List<ListViewTemplate> listCornerStones = new List<ListViewTemplate>
            {
             new ListViewTemplate
             {
                 Icon="Breathwork_Awareness.png",
                 Name="BREATH BREATH AWARENESS",
                 Desc="",
                 Count=0,

             },

             new ListViewTemplate
             {
                 Icon="Breathwork_Relaxation.png",
                 Name="RELAXATION LET GO!",
                 Desc="",
                 Count=0,

             },

             new ListViewTemplate
             {
                 Icon="Breathwork_TakCharge.png",
                 Name="ENERGY TAKE CHARGE",
                 Desc="",
                 Count=0,

             }
            };
            ItemListView.ItemsSource = listCornerStones;
            ItemListView.HeightRequest = listCornerStones.Count * 200;
            myList = listCornerStones;
            //ItemListView.Height = Convert.ToDouble(3 * 65);
            //return new ListViewTemplate();
        }

        private async void BasicBreathing()
        {
            
            List < ListViewTemplate > listBasicBreathing = new List<ListViewTemplate>
            {
                   new ListViewTemplate
                   {
                      Icon="EstablishRythm.png",
                      Name="ESTABLISHING A RHYTHM",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="NoseVsMouth.png",
                      Name="NOSE VS MOUTH BREATHING",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="EnergyToEffor.png",
                      Name="EFFORT TO ENERGY RATIO",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="DiaphramaticBreathing.png",
                      Name="DIAPHRAMATIC BREATHING",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="threeBreathingSpacesYogicBreath.png",
                      Name="THE 3 BREATHING SPACES",
                      Desc="",
                      Count=0
                   },

                   new ListViewTemplate
                   {
                      Icon="FastSlow.png",
                      Name="FAST SLOW HIGH LOW",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="CoherentBreathing.png",
                      Name="COHERENT BREATHING",
                      Desc="",
                      Count=0
                   },
                   new ListViewTemplate
                   {
                      Icon="InfusingEnergyCell.png",
                      Name="INFUSING EVERY CELL",
                      Desc="",
                      Count=0
                   }
            };
            ItemListView.ItemsSource = listBasicBreathing;
            ItemListView.HeightRequest = listBasicBreathing.Count * 200;
            myList = listBasicBreathing;
            //return new ListViewTemplate();
        }
        private async void EverydayBreathing()
        {
            List<ListViewTemplate> listEverydayBreathing = new List<ListViewTemplate>
            {
             new ListViewTemplate
             {
                 Icon="MorningRitual.png",
                 Name="MORNING RITUAL",
                 Desc="",
                 Count=0

             },
             new ListViewTemplate
             {
                Icon="StandingInLine.png",
                 Name="STANDING IN LINE",
                 Desc="",
                 Count=0
             },
             new ListViewTemplate
             { Icon="BoredomAndDistraction.png",
              Name="BOREDOM AND DISTRACTION",
                 Desc="",
                 Count=0
             },
             new ListViewTemplate
             {
                 Icon="BusyMind.png",
                  Name="BUSY MIND",
                 Desc="",
                 Count=0
             },

             new ListViewTemplate
             {
                  Icon="HeadacheBreath.png",
                   Name="HEADACHE BREATH",
                 Desc="",
                 Count=0
             },

             new ListViewTemplate
             {
                  Icon="CoolingDown.png",
                   Name="COOLING DOWN",
                 Desc="",
                 Count=0
             },
             new ListViewTemplate
             {
                  Icon="HangoverBreath.png",
                   Name="HANGOVER BREATH",
                 Desc="",
                 Count=0
             }

            };
            ItemListView.ItemsSource = listEverydayBreathing;
            ItemListView.HeightRequest = listEverydayBreathing.Count * 200;
            myList = listEverydayBreathing;
            //return new ListViewTemplate();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {

            if(!string.IsNullOrEmpty(searchBar.Text))
            {
                var searchResult = myList.Where(c => c.Name.Contains(searchBar.Text.ToUpper()));
                ItemListView.ItemsSource = searchResult;
            }
            else
            {
                ItemListView.ItemsSource = myList;
            }
            
        }

        private async void FindYourPath()
        {
            string Heading = "Find Your Path";
            await Navigation.PushAsync(new SubMenuListViewTemplate(Heading)); 
        }

        private async void ItemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as ListViewTemplate;

            var In = new ListViewTemplate();

            switch (selected.Icon)
            {

                //Basic Breathing
                case "EstablishRythm.png":
                    icon = "EstablishRythm.png";
                    name = "ESTABLISHING";
                    desc = "A RHYTHM";
                    In.Heading = "ESTABLISHING A RHYTHM";
                    In.IntroText = "“Inhale, exhale, pause... inhale, exhale, pause...” This is nature’s way of breathing our body when we are at rest. “Inhale, exhale, pause…” describes our normal, healthy, chemically driven, biologically-based breathing pattern. Notice that it is not “inhale, pause, exhale.”";// The pause comes after the exhale phase; and we need to make that pause a comfort zone: like coming home after each movement and cycle of breath.";
                    In.Url_Text = "Basic Series - The Natural Breathing Rhythm.html";
                    In.Url_Aud = "Breathwork_Establish a rhythm.mp3";
                    In.Url_Vid = "Establish a rythm.mp4";

                    break;


                case "NoseVsMouth.png":
                    icon = "NoseVsMouth.png";
                    name = "NOSE VS MOUTH";
                    desc = "BREATHING";
                    In.Heading = "NOSE VS MOUTH BREATHING";
                    In.IntroText = "The nose and mouth represent the two channels that breath can use to come and go. The nose is meant for breathing: it has hairs that filter dust, mucus membranes that trap microscopic particles. It warms or cools the air, conditioning it; and it has structures that spiral the air as it enters the lungs.";
                    In.Url_Text = "Basic Series - Nose Breathing VS Mouth Breathing.html";
                    In.Url_Aud = "Breathwork_Mouth Breathing.mp3";
                    In.Url_Vid = "Nose Vs Mouth Breathing.mp4";

                    break;

                case "EnergyToEffor.png":
                    icon = "EnergyToEffor.png";
                    name = "EFFORT TO ENERGY";
                    desc = "RATIO";
                    In.Heading = "EFFORT TO ENERGY RATIO";
                    In.IntroText = "The principle here is to make sure that you are not wasting energy in the process of breathing. Make sure that you are not expending any unnecessary muscular effort when you breathe consciously. The diaphragm and the intercostal muscles (between the ribs) are your primary and secondary breathing muscles.";
                    In.Url_Text = "Basic Series - Energy to Effort Ratio.html";
                    In.Url_Aud = "Breathwork_Energy2Effort.mp3";
                    In.Url_Vid = "Effort To Energy Ratio.mp4";

                    break;
                case "DiaphramaticBreathing.png":
                    icon = "DiaphramaticBreathing.png";
                    name = "DIAPHRAGMATIC";
                    desc = "BREATHING";
                    In.Heading = "DIAPHRAGMATIC BREATHING";
                    In.IntroText = "The diaphragm is a dome shaped sheet of muscle that separates the chest from the abdomen. It serves as the primary breathing muscle. As the lungs inflate on the inhalation the diaphragm flattens pushing the belly out and on exhalation, the diaphragm relaxes back up towards the ribcage and the belly flattens."; // Diaphragmatic breathing is also called “belly breathing” because of the natural movement of the belly when breathing";
                    In.Url_Text = "Basic series - Diaphragmatic Breathing.html";
                    In.Url_Aud = "Breathwork_Diaphragmatic.mp3";
                    In.Url_Vid = "Diaphramatic Breathing.mp4";

                    break;
                case "threeBreathingSpacesYogicBreath.png":
                    icon = "threeBreathingSpacesYogicBreath.png";
                    name = "THREE BREATHING SPACES";
                    desc = "";
                    In.Heading = "THREE BREATHING SPACES";
                    In.IntroText = "Consider that you have three distinct breathing spaces: a lower breathing space from the perineum to the belly button, a middle breathing space from the belly button to the nipple line, and an upper breathing space from the nipple line to the collarbones.";
                    In.Url_Text = "Basic Series - Three Breathing Spaces and the Full Yogic Breath.html";
                    In.Url_Aud = "Breathwork_Yogic breath.mp3";
                    In.Url_Vid = "Three Breathing Spaces.mp4";

                    break;
                case "FastSlow.png":
                    icon = "FastSlow.png";
                    name = "FAST SLOW";
                    desc = "HIGH LOW";
                    In.Heading = "FAST SLOW BREATHING";
                    In.IntroText = "This practice is about exploring another basic parameter in breathing. A healthy person should be able to breathe quickly: 60, 120, or even 180 breaths per minute. And a healthy person should be able to breathe slowly: 3, 2, or even 1 breath per minute or less.";
                    In.Url_Text = "Basic Series - Fast and Slow.html";
                    In.Url_Aud = "Breathwork_Fast low high low.mp3";
                    In.Url_Vid = "Fast Slow High Low.mp4";

                    break;
                case "CoherentBreathing.png":
                    icon = "CoherentBreathing.png";
                    name = "COHERENT";
                    desc = "BREATHING";
                    In.Heading = "COHERENT BREATHING";
                    In.IntroText = "This practice is about balancing the sympathetic and parasympathetic nervous system. The technique is simple: breathe in for five seconds and breathe out for five seconds… In for five, out for five… inhale five, exhale five… Gently, continuously, consciously. Simple.";
                    In.Url_Text = "Basic Series - Coherent Breathing.html";
                    In.Url_Aud = "Breathwork_Coherent Breathing.mp3";
                    In.Url_Vid = "Coherent Breathing.mp4";

                    break;
                case "InfusingEnergyCell.png":
                    icon = "InfusingEnergyCell.png";
                    name = "INFUSING";
                    desc = "EVERY CELLS";
                    In.Heading = "INFUSING EVERY CELL";
                    In.IntroText = "There’s more to breathing then just moving air in and out of the lungs. Breath-Energy needs to get to every cell of your body. And so the practice is to consciously send breath-energy to every part of you.";
                    In.Url_Text = "Basic Series - Infusing Every Cell.html";
                    In.Url_Aud = "Breathwork_Infusing cells.mp3";
                    In.Url_Vid = "Infusing Every Cell.mp4";

                    break;


                //Cornerstones

                case "Breathwork_Awareness.png":
                    icon = "Breathwork_Awareness.png";
                    name = "BREATH";
                    desc = "BREATH AWARENESS";
                    In.Heading = "BREATH AWARENESS";
                    In.IntroText = "Most of the time we are not conscious of our breathing, because we don’t have to be. Our breath is under the control of our autonomic nervous system that also regulates our heart rate and temperature which maintains homeostatsis and balance.";
                    In.Url_Text = "Conerstones - Breath_awareness.html";
                    In.Url_Aud = "Breathwork_Awareness.mp3";
                    In.Url_Vid = "cornerstone_one_breath_awareness.mp4";



                    break;

                case "Breathwork_Relaxation.png":
                    icon = "Breathwork_Relaxation.png";
                    name = "RELAXATION";
                    desc = "LET GO!";
                    In.Heading = "RELAXATION";
                    In.IntroText = "True relaxation is the forgotten art of the 21st century and the new paradigm in peak performance. More work is done in a state of relaxation than we realise. Being in a constant state of ‘high energy’ or adrenalized energy is useful in the short term but toxic to the system in the long term."; // Most of us are holding habitual patterns of tension that need to be unravelled so that energy can flow effectively throughout our system.";
                    In.Url_Text = "Conerstones - Relaxation.html";
                    In.Url_Aud = "Breathwork_Relaxation.mp3";
                    In.Url_Vid = "Cornerstone 2 - Relaxation.mp4";
                    break;


                case "Breathwork_TakCharge.png":
                    icon = "Breathwork_TakCharge.png";
                    name = "ENERGY";
                    desc = "TAKE CHARGE";
                    In.Heading = "ENERGY";

                    In.IntroText = "We use breath control to access more energy and more power and aliveness. This exercise is about experiencing the breath as life force energy and getting comfortable with intensity. In the last exercise we engaged the exhale to deepen relaxation.";//In this foundational exercise, we are engaging the inhale to activate energy in the system.";
                    In.Url_Text = "Conerstones - Energy.html";
                    In.Url_Aud = "Breathwork_Energy.mp3";
                    In.Url_Vid = "Extra Energy.mp4";
                    break;

                // EverydayBreathng

                case "MorningRitual.png":
                    icon = "MorningRitual.png";
                    name = "MORNING";
                    desc = "RITUAL";
                    In.Heading = "MORNING RITUAL";
                    In.IntroText = "What is the first thing you do when you wake up? Turn off your alarm? Check your for phone or messages, emails and twitter feeds? Training the habit of tuning into your breath and body as the first thing you do in the morning is a great way to begin your day mindfully and with authentic energy.";
                    In.Url_Text = "Busy Mind - Morning_Ritual.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Morning Ritual.mp4";

                    break;

                case "StandingInLine.png":
                    icon = "StandingInLine.png";
                    name = "STANDING";
                    desc = "IN LINE";
                    In.Heading = "STANDING IN LINE";
                    In.IntroText = "Standing in line or waiting in a queue is an unavoidable part of the human experience and often one that triggers frustration, irritability and impatience. However it could be a great opportunity to shift perspective and practice kindness, empathy and compassion.";
                    In.Url_Text = "Busy Mind - Standing_in_line.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Standing In Line.mp4";
                    break;


                case "BoredomAndDistraction.png":
                    icon = "BoredomAndDistraction.png";
                    name = "BOREDOM AND";
                    desc = "DISTRACTION";
                    In.Heading = "BOREDOM AND DISTRACTION";
                    In.IntroText = "We have all had the experience of being distracted when what is required is focus and concentration. Boredom is nothing more than a distraction from the present moment experience and the inability to relax into it.";
                    In.Url_Text = "Busy Mind - Boredom_and_Distraction.html";
                    In.Url_Aud = "Breathwork_Boredom.mp3";
                    In.Url_Vid = "Boredom and Distraction.mp4";
                    break;

                case "BusyMind.png":
                    icon = "BusyMind.png";
                    name = "BUSY";
                    desc = "MIND";
                    In.Heading = "BUSY MIND";
                    In.IntroText = "Often times we only realize how busy the mind is once we start to slow down, relax or we want to fall asleep. Our relaxation is hindered by the mind that seems to have a life of its own.";
                    In.Url_Text = "Busy Mind - Busy_Mind.html";
                    In.Url_Aud = "Breathwork_Busy Mind.mp3";
                    In.Url_Vid = "Busy Mind.mp4";
                    break;

                case "HeadacheBreath.png":
                    icon = "HeadacheBreath.png";
                    name = "HEADACHE";
                    desc = "BREATH";
                    In.Heading = "HEADACHE BREATH";
                    In.IntroText = "This technique is not necessarily about making the headache or pain go away, it’s more about learning to be comfortable and to relax in the presence of pain."; // Exploring the pain through the breath often brings to awareness unconscious habits that may be causing or exacerbating the pain, for example tension in the jaw, postural issues, emotional tendencies or thought patterns.";
                    In.Url_Text = "Breathing - Headache Breath.html";
                    In.Url_Aud = "Breathwork_Headache Breath.mp3";
                    In.Url_Vid = "Headache Breath.mp4";
                    break;
                case "CoolingDown.png":
                    icon = "CoolingDown.png";
                    name = "COOLING";
                    desc = "DOWN";
                    In.Heading = "COOLING DOWN";
                    In.IntroText = "We can use the breath to warm up our hands or to cool off our soup. The temperature of the breath changes in relation to the shape of the stream of breath that we create with our mouth. This technique comes from the yoga tradition and is called ‘Sitali Breath’.";
                    In.Url_Text = "Breathing - Cooling Down.html";
                    In.Url_Aud = "Breathwork_Cooling Down.mp3";
                    In.Url_Vid = "Cool Down.mp4";
                    break;
                case "HangoverBreath.png":
                    icon = "HangoverBreath.png";
                    name = "HANGOVER";
                    desc = "BREATH";
                    In.Heading = "HANGOVER BREATH";
                    In.IntroText = "The body is a self - healing and self-detoxifying system. Sometimes, we indulge behaviors that overwhelm or tax this system. In these cases, we have to step in and support the body to do what it is designed to do."; // Oxygen increases the rate at which toxins are processed in the body, and so conscious breathing is a simple and effective way to support the body’s detox function.";
                    In.Url_Text = "Breathing - Hangover Breath.html";
                    In.Url_Aud = "Breathwork_Hangover Breath.mp3";
                    In.Url_Vid = "Hangover Breath.mp4";
                    break;

               
            }

            
                await Navigation.PushAsync(new IntemediaryView(In.Heading, In.IntroText, icon, Heading.Text, desc, In.Url_Text, In.Url_Aud, In.Url_Vid));
            

        }
        //protected override bool OnBackButtonPressed()
        //{
        //    base.OnBackButtonPressed();
        //    //GoBack();
        //    return true;
        //}

        private async void Close_Clicked(object sender, EventArgs e)
        {
            var x = Navigation.NavigationStack.Count;
            if (x == 2)
                await Navigation.PopAsync(true);
            else
            await Navigation.PushAsync(new MainPage(false));
        }
        private async void Search_Pressed(object sender, EventArgs e)
        {
            searchBar.IsVisible = true;
            //search.IsVisible = false;
            //await PopupNavigation.PushAsync(new SearchPopup());
        }
    }
}