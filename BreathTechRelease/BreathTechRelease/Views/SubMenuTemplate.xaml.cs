using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BreathTechRelease.Views;
using BreathTechRelease.Models;
using Xamarin.Essentials;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubMenuTemplate : ContentPage
    {
        public string icon, name, desc, count;
        List<ListViewTemplate> listViewTemplates;
        List<ListViewTemplate> myList;

        public SubMenuTemplate(string icn, string dsc)
        {
            InitializeComponent();
            string type = Preferences.Get("subscriptionType", "");
            string isSubscriptionActive = Preferences.Get("SubscriptionActive", "");
            /*if (type == "Free" || isSubscriptionActive == "0")
                //searchImage.IsVisible = false;
                searchImage.Source = "";*/
            ItemListView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
              ((ListView)sender).SelectedItem = null;
            };
            Heading.Text = dsc;

            string Choice = icn;

            switch (Choice)
            {
                case "FindYourPath_Bodywise.png":
                    listViewTemplates = new List<ListViewTemplate>
            {
             new ListViewTemplate
            {
                Icon="Bodywise_Hypertension.png",
                Name="HYPERTENSION",
                Desc="",
                Count=0,
                Id=11
            },

             new ListViewTemplate
            {
                Icon="Bodywise_Pain.png",
                Name="BREATHING FOR PAIN",
                Desc="",
                Count=0,
                Id=12
            },

            new ListViewTemplate
            {
                Icon="Bodywise_Fatigue.png",
                Name="BREATHING FOR FATIGUE",
                Desc="",
                Count=0,
                Id=13
            },
            new ListViewTemplate
            {
                Icon="Bodywise_Insomnia.png",
                Name="INSOMNIA",
                Desc="",
                Count=0,
                Id=14
            },

            new ListViewTemplate
            {
                Icon="Bodywise_Headaches.png",
                Name="HEADACHES",
                Desc="",
                Count=0,
                Id=15
            },
            
            new ListViewTemplate
            {
                Icon="Bodywise_Weightloss.png",
                Name="WEIGHT LOSS",
                Desc="",
                Count=0,
                Id=16
            },
            new ListViewTemplate
            {
                Icon="Bodywise_Nausea.png",
                Name="NAUSEA",
                Desc="",
                Count=0,
                Id=17
            },
            new ListViewTemplate
            {
                Icon="Bodywise_Asthma.png",
                Name="ASTHMA",
                Desc="",
                Count=0,
                Id=18
            },
            new ListViewTemplate
            {
                Icon="Bodywise_IrritableBowelSyndrome.png",
                Name="IRRITABLE BOWEL SYNDROME",
                Desc="",
                Count=0,
                Id=19
            }
            };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;
                case "FindYourPath_BusinessEdge.png":
                    listViewTemplates = new List<ListViewTemplate>
                    { new ListViewTemplate
                    {
                        Icon="BusinessEdge_BoostingEnergy.png",
                        Name="BOOSTING ENERGY",
                        Desc="",
                        Count=0,
                        Id=21
                    },

                     new ListViewTemplate
                    {
                        Icon="BusinessEdge_BuildingPersonalPresence.png",
                        Name="BUILDING PERSONAL PRESENCE",
                        Desc="",
                        Count=0,
                         Id=22
                    },

                      new ListViewTemplate
                    {
                        Icon="BusinessEdge_CreatingPerspective.png",
                        Name="CREATING PERSPECTIVE",
                        Desc="",
                        Count=0,
                        Id=23
                    },

                       new ListViewTemplate
                    {
                        Icon="BusinessEdge_DealingwithConflict.png",
                        Name="DEALING WITH CONFLICT",
                        Desc="",
                        Count=0,
                        Id=24
                    },

                        new ListViewTemplate
                    {
                        Icon="BusinessEdge_Preaparingforapresentation.png",
                        Name="PREPARING FOR A PRESENTATION",
                        Desc="",
                        Count=0,
                        Id = 25
                    },

                         new ListViewTemplate
                    {
                        Icon="BusinessEdge_StartingandEndingyourworkday.png",
                        Name="STARTING AND ENDING YOUR WORK DAY",
                        Desc="",
                        Count=0,
                        Id=26
                    },
                                  new ListViewTemplate
                    {
                        Icon="BusinessEdge_Worklifeintegration.png",
                        Name="WORK LIFE INTEGRATION",
                        Desc="",
                        Count=0,
                        Id=27
                    },
                        new ListViewTemplate
                    {
                        Icon="BusinessEdge_ConnstructiveListening.png",
                        Name="CONSTRUCTIVE LISTENING",
                        Desc="",
                        Count=0,
                        Id=28

                    },

                       new ListViewTemplate
                    {
                        Icon="BusinessEdge_FeelingOverwhelmed.png",
                        Name="FEELING OVERWHELMED",
                        Desc="",
                        Count=0,
                        Id=29
                    },

                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;



                case "FindYourPath_HeartIntelligence.png":

                    listViewTemplates = new List<ListViewTemplate>

                    {
                        new ListViewTemplate
                    {
                        Icon="HeartIntelligence_FeelingOverwhelmed.png",
                        Name="FEELING OVERWHELMED",
                        Desc="",
                        Count=0,
                        Id=31
                    },
                        new ListViewTemplate
                    {
                        Icon="HeartIntelligence_Anxiety.png",
                        Name="ANXIETY",
                        Desc="",
                        Count=0,
                        Id=32
                    },
                    new ListViewTemplate
                    {
                        Icon="HeartIntelligence_Anger.png",
                        Name="ANGER",
                        Desc="",
                        Count=0,
                        Id=33
                    },
                    
                    new ListViewTemplate
                    {
                        Icon="HeartIntelligence_Depression.png",
                        Name="DEPRESSION",
                        Desc="",
                        Count=0,
                        Id=34
                    },
                       new ListViewTemplate
                    {
                        Icon="HeartIntelligence_Grief.png",
                        Name="GRIEF",
                        Desc="",
                        Count=0,
                        Id=35
                    },

                    new ListViewTemplate
                    {
                        Icon="HeartIntelligence_FeelingAlone.png",
                        Name="FEELING ALONE",
                        Desc="",
                        Count=0,
                        Id=36
                    }
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;

                case "FindYourPath_GoforGold.png":
                    listViewTemplates = new List<ListViewTemplate>
                    { new ListViewTemplate
                    {
                        Icon="GoforGold_TwoPhaseBreathing.png",
                        Name="2 PHASE BREATHING",
                        Desc="",
                        Count=0,
                        Id=41
                    },

                     new ListViewTemplate
                    {
                        Icon="GoforGold_BetweenSets.png",
                        Name="BETWEEN SETS",
                        Desc="",
                        Count=0,
                        Id=42
                    },
                      new ListViewTemplate
                    {
                        Icon="GoforGold_BreathHolding.png",
                        Name="BREATH HOLD TRAINING",
                        Desc="",
                        Count=0,
                        Id=43
                    },
                       new ListViewTemplate
                    {
                        Icon="GoforGold_Leadingwiththebreath.png",
                        Name="LEADING WITH BREATH",
                        Desc="",
                        Count=0,
                        Id=44
                    },
                        new ListViewTemplate
                    {
                        Icon="GoforGold_PreparationforPeakPerformance.png",
                        Name="PREPARING FOR PEAK PEFORMANCE",
                        Desc="",
                        Count=0,
                        Id=45
                    },
                         new ListViewTemplate
                    {
                        Icon="GoforGold_ReverseRespiration.png",
                        Name="REVERSE RESPIRATION",
                        Desc="",
                        Count=0,
                        Id=46
                    },
                          new ListViewTemplate
                    {
                        Icon="GoforGold_TeamBreathing.png",
                        Name="TEAM BREATHING",
                        Desc="",
                        Count=0,
                        Id=47
                    },
                           new ListViewTemplate
                    {
                        Icon="GoforGold_WimHofMethod.png",
                        Name="THE WIM HOF METHOD",
                        Desc="",
                        Count=0,
                        Id=48
                    }
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;

                case "FindYourPath_BeYourBest.png":
                    listViewTemplates = new List<ListViewTemplate>
                    {
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_artofmanifestation.png",
                        Name="ART OF",
                        Desc="MANIFESTATION",
                        Count=0,
                        Id=51
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_Confidence.png",
                        Name=" CONFIDENCE",
                        Desc="",
                        Count=0,
                        Id=52
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_Connectingtoselfandothers.png",
                        Name="CONNECTING TO SELF AND OTHERS",
                        Desc="",
                        Count=0,
                        Id=53
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_FeelingAmazing.png",
                        Name="FEELING AMAZING",
                        Desc="",
                        Count=0,
                        Id=54
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_FocusandAttention.png",
                        Name="FOCUS AND ATTENTION",
                        Desc="",
                        Count=0,
                        Id=55
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_FountainBreath.png",
                        Name="FOUNTAIN BREATH",
                        Desc="",
                        Count=0,
                        Id=56
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_Listeningfrontheheart.png",
                        Name="LISTENING TO THE HEART",
                        Desc="",
                        Count=0,
                        Id=57
                    },
                    new ListViewTemplate
                    {
                        Icon="BeYourbest_YesBreath.png",
                        Name="YES BREATH",
                        Desc="",
                        Count=0,
                        Id=58
                    }
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;


                case "FindYourPath_MakingMagic.png":
                    listViewTemplates = new List<ListViewTemplate>
                    { new ListViewTemplate
                    {
                        Icon="MakingMagic_BreathingSandwich.png",
                        Name="CREATING MAGIC",
                        Desc="",
                        Count=0,
                        Id=61
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_FountainBreath.png",
                        Name="FOUNTAIN BREATH",
                        Desc="",
                        Count=0,
                        Id=62
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_Playingwiththechannel.png",
                        Name="PLAYING WITH THE CHANNEL",
                        Desc="",
                        Count=0,
                        Id=63
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_Precisionunderpressure.png",
                        Name="PRECISION UNDER PRESSURE",
                        Desc="",
                        Count=0,
                        Id=64
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_QuickBoostofEnergyandInspiration.png",
                        Name="QUICK BOOST OF ENERGY",
                        Desc="",
                        Count=0,
                        Id=65
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_Tappingintothesourceofecstatic.png",
                        Name="ECSTATIC ENERGY",
                        Desc="",
                        Count=0,
                        Id=66
                    },
                    new ListViewTemplate
                    {
                        Icon="MakingMagic_UnblockingCreativeEnergy.png",
                        Name="UNBLOCKING CREATIVE ENERGY",
                        Desc="",
                        Count=0,
                        Id=67
                    }
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;
                case "FindYourPath_HelpingtheHelper.png":
                    listViewTemplates = new List<ListViewTemplate>
                    { new ListViewTemplate
                    {
                        Icon="HelpingtheHelper_Cleansingyourfield.png",
                        Name="CLEANSING YOUR ENERGY",
                        Desc="",
                        Count=0,
                        Id=71
                    },

                    new ListViewTemplate
                    {
                        Icon="HelpingtheHelper_Compassionthroughconnecting.png",
                        Name="COMPASSION THROUGH CONNECTION",
                        Desc="",
                        Count=0,
                        Id=72
                    },

                    new ListViewTemplate
                    {
                        Icon="HelpingtheHelper_Listeningwithlove.png",
                        Name="LISTENING WITH LOVE",
                        Desc="",
                        Count=0,
                        Id=73
                    },

                    new ListViewTemplate
                    {
                        Icon="HelpingtheHelperr_LovingKindness.png",
                        Name="LOVING KINDNESS",
                        Desc="",
                        Count=0,
                        Id=74
                    },

                    new ListViewTemplate
                    {
                        Icon="HelpingtheHelper_Shakingitout.png",
                        Name="SHAKING IT OUT",
                        Desc="",
                        Count=0,
                        Id=75
                    },

                    new ListViewTemplate
                    {
                        Icon="HelpingtheHelper_SpiritualVacuumCleaner.png",
                        Name="SPIRITUAL VACUUM CLEANING",
                        Desc="",
                        Count=0,
                        Id=76
                    },
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;

                case "FindYourPath_SoulSource.png":

                    listViewTemplates = new List<ListViewTemplate>
                    { new ListViewTemplate
                    {
                        Icon="SoulSorce_ConsciousConnectedBreathing.png",
                        Name="CONSCIOUS CONNECTED BREATHING",
                        Desc="",
                        Count=0,
                        Id=81
                    },
                     new ListViewTemplate
                    {
                        Icon="SoulSorce_CosmicBreaths.png",
                        Name="COSMIC BREATHS",
                        Desc="",
                        Count=0,
                        Id=82
                    },
                      new ListViewTemplate
                    {
                        Icon="SoulSorce_DereflexiveBreathing.png",
                        Name="DE-RELEXIVE BREATHING",
                        Desc="",
                        Count=0,
                        Id=83
                    },
                       new ListViewTemplate
                    {
                        Icon="SoulSorce_MicrocosmicOrbit.png",
                        Name="MICRO COSMIC ORBIT",
                        Desc="",
                        Count=0,
                        Id=84
                    },
                        new ListViewTemplate
                    {
                        Icon="SoulSorce_PrayerBreath.png",
                        Name="PRAYER BREATH",
                        Desc="",
                        Count=0,
                        Id=85
                    },
                         new ListViewTemplate
                    {
                        Icon="SoulSorce_RelaxedSubtleenergybreathing.png",
                        Name="RELAXED SUBTLE ENERGY BREATHING",
                        Desc="",
                        Count=0,
                        Id=86
                    },
                          new ListViewTemplate
                    {
                        Icon="SoulSorce_Threewavesofbreathing.png",
                        Name="THE WAVES OF ENERGY",
                        Desc="",
                        Count=0,
                        Id=87
                    },
                    };
                    ItemListView.ItemsSource = listViewTemplates;
                    ItemListView.HeightRequest = listViewTemplates.Count * 200;
                    myList = listViewTemplates;
                    break;
            }

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

        private async void ItemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var selected = e.Item as ListViewTemplate;

            var In = new ListViewTemplate();


            switch (selected.Id)
            {
                case 11:
                    icon = "Bodywise_Hypertension.png";
                    name = "";
                    desc = "";
                    In.Heading = "HYPERTENSION";
                    In.IntroText = "Hypertension is caused by an increase in arterial tone and sympathetic overdrive. It is often associated with a breathing pattern that is focused high in the chest and with a fast respiratory rate. The basic principle of this technique is to decrease the sympathetic response and arterial tone by breathing ‘low and slow’.";
                    In.Url_Text = "Bodywise - Hypertension.html";
                    In.Url_Aud = "Breathwork_Hypertension.mp3";
                    In.Url_Vid = "Hypertension.mp4";
                    break;

                case 12:
                    icon = "Bodywise_Pain.png";
                    name = "";
                    desc = "";
                    In.Heading = "PAIN";
                    In.IntroText = "Our mind reacts or responds to our experiences. Likewise, our breathing can either react or respond to pain. Our breath often reacts to pain by locking up and or by becoming fast and shallow. You will notice how your breathing changes when you are in pain. ";
                    In.Url_Text = "Bodywise - Breathing for Pain.html";
                    In.Url_Aud = "Breathwork_Pain.mp3";
                    In.Url_Vid = "Breathing For Pain.mp4";
                    break;

                case 13:
                    icon = "Bodywise_Fatigue.png";
                    name = "";
                    desc = "";
                    In.Heading = "FATIGUE";
                    In.IntroText = "Fatigue could be caused by any number of reasons from mental exhaustion, emotional factors to physical fatigue from exertion, illness or sleep disturbances. If you can identify the nature of the fatigue, you can adapt this exercise accordingly.";
                    In.Url_Text = "Bodywise - Breathing for Fatigue.html";
                    In.Url_Aud = "Breathwork_Fatigue.mp3";
                    In.Url_Vid = "Breathing For Fatigue.mp4";
                    break;

                case 14:
                    icon = "Bodywise_Insomnia.png";
                    name = "";
                    desc = "";
                    In.Heading = "INSOMNIA";
                    In.IntroText = "Sleep is a fundamental pillar of health and energy and when this is compromised, every other function in the body is affected. Often sleep disturbances are caused by too much ‘adrenalized energy’, physical tension, pain or digestive disturbances.";
                    In.Url_Text = "Bodywise - Insomnia.html";
                    In.Url_Aud = "Insomnia.mp3";
                    In.Url_Vid = "Insomnia.mp4";
                    break;

                case 15:
                    icon = "Bodywise_Headaches.png";
                    name = "";
                    desc = "";
                    In.Heading = "HEADACHE";
                    In.IntroText = "This technique is not necessarily about making the headache or pain go away, it’s more about learning to be comfortable and to relax in the presence of pain. ";
                    In.Url_Text = "Bodywise - Headaches.html";
                    In.Url_Aud = "Breathwork_Headache Breath.mp3";
                    In.Url_Vid = "Headache Breath.mp4";
                    break;

                case 16:
                    icon = "Bodywise_Weightloss.png";
                    name = "";
                    desc = "";
                    In.Heading = "WEIGHT LOSS";
                    In.IntroText = "Optimal breathing plays a fundamental role in weight management. Oxygen is a vital part of the chemistry of fat burning and 80% of the metabolic byproducts of fat burning is excreted as carbon dioxide.";
                    In.Url_Text = "Bodywise - Weight loss.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Weight Loss.mp4";
                    break;

                case 17:
                    icon = "Bodywise_Nausea.png";
                    name = "";
                    desc = "";
                    In.Heading = "NAUSEA";
                    In.IntroText = "Most of the breathing techniques that we’ve been practicing have involved deep full slow breaths. But deep breathing can often exacerbate nausea, therefore managing the symptoms of nausea asks for a slightly different approach.";
                    In.Url_Text = "Bodywise - Nausea.html";
                    In.Url_Aud = "Breathwork_Nausea.mp3";
                    In.Url_Vid = "Nausea.mp4";
                    break;

                case 18:

                    icon = "Bodywise_Asthma.png";
                    name = "";
                    desc = "BODYWISE";
                    In.Heading = "ASTHMA";
                    In.IntroText = "While there are many triggers for asthma: environmental, dietary, emotional and exercise induced, the effect on the airways is the same. The smooth muscles in the airway walls contract, causing a narrowing of the airway. ";
                    In.Url_Text = "Bodywise - Asthma.html";
                    In.Url_Aud = "Breathwork_Asthma.mp3";
                    In.Url_Vid = "Asthma.mp4";
                    break;

                case 19:
                    icon = "Bodywise_IrritableBowelSyndrome.png";
                    name = "";
                    desc = "";
                    In.Heading = "IBS";
                    In.IntroText = "IBS is one of the most common digestive disorders often triggered by stress, anxiety and food intolerances. ";
                    In.Url_Text = "Bodywise - Irritable Bowel Syndrome.html";
                    In.Url_Aud = "Breathwork_IBS.mp3";
                    In.Url_Vid = "Segment 59.mp4";
                    break;
                
                //business edge

                case 21:
                    icon = "BusinessEdge_BoostingEnergy.png";
                    name = "";
                    desc = "";
                    In.Heading = "BOOSTING ENERGY";
                    In.IntroText = "Use this energy boost in the middle of the working day when you can't afford to take time out, or anytime you begin to feel dull or tired.";
                    In.Url_Text = "Texts for business - Boosting energy.html";
                    In.Url_Aud = "Breathwork_Energy.mp3";
                    In.Url_Vid = "Extra Energy.mp4";
                    break;
                case 22:
                    icon = "BusinessEdge_BuildingPersonalPresence.png";
                    name = "";
                    desc = "";
                    In.Heading = "PERSONAL PRESENCE";
                    In.IntroText = "Many people seem to have a natural ability to control a room, to influence, inspire or lead others. This is sometimes referred to as power or charisma. The fact is, anyone can develop this ability with practice.";
                    In.Url_Text = "Texts for business - Building personal presence.html";
                    In.Url_Aud = "Breathwork_Personal Presence.mp3";
                    In.Url_Vid = "";
                    break;
                case 23:
                    icon = "BusinessEdge_CreatingPerspective.png";
                    name = "";
                    desc = "";
                    In.Heading = "CREATE PERSPECTIVE";
                    In.IntroText = "Many business leaders at some point lose sight of the big picture or their original focus and intention as they get caught up in the nitty gritty details, reacting to daily issues, or find themselves having to manage unplanned events and challenges.";
                    In.Url_Text = "Texts for business - Creating perspective.html";
                    In.Url_Aud = "Breathwork_Perspective.mp3";
                    In.Url_Vid = "Segment 61.mp4";
                    break;
                case 24:
                    icon = "BusinessEdge_DealingwithConflict.png";
                    name = "";
                    desc = "";
                    In.Heading = "DEAL WITH CONFLICT";
                    In.IntroText = "When the inevitable work and life and interpersonal conflicts occur, we need to understand our triggers, our reaction to the triggers and how to communicate from an empowered yet peaceful place. We are called to remain conscious and centered, to balance peace and power.";
                    In.Url_Text = "Texts for business - Dealing with conflict.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 93.mp4";
                    break;
                case 25:
                    icon = "BusinessEdge_Preaparingforapresentation.png";
                    name = "";
                    desc = "";
                    In.Heading = "PRESENTATION BREATH";
                    In.IntroText = "Before a presentation, the idea is to boost your energy and to harness it so that you can deliver your message with clarity and confidence. You also want to calm any anxiety in order to project confidence. ";
                    In.Url_Text = "Texts for business - Preparing for a presentation 9 Box breathing.html";
                    In.Url_Aud = "Breathwork_Presentation.mp3";
                    In.Url_Vid = "Preperation breath.mp4";
                    break;
                case 26:
                    icon = "BusinessEdge_StartingandEndingyourworkday.png";
                    name = "";
                    desc = "";
                    In.Heading = "YOUR WORK DAY";
                    In.IntroText = "Create a breathing ritual before, after and midway through your work day. Use the well proven breathwork practice called 365. This means you will practice breathing at a rate of six breaths per minute, for five minutes, three times per day.";
                    In.Url_Text = "Texts for business - Starting and ending your work day.html";
                    In.Url_Aud = "Breathwork_365 Technique.mp3";
                    In.Url_Vid = "Segment 17.mp4";
                    break;
                case 27:
                    icon = "BusinessEdge_Worklifeintegration.png";
                    name = "";
                    desc = "";
                    In.Heading = "WORK/LIFE INTEGRATION";
                    In.IntroText = "In today’s demanding fast-paced, world, it’s easy to get out of balance; it’s easy to find that our home life can suffer because of our concentration on work, or that our work life can suffer because of family problems, obligations or distractions.";
                    In.Url_Text = "Texts for business - Work Life integration.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 43.mp4";
                    break;
                case 28:
                    icon = "BusinessEdge_ConnstructiveListening.png";
                    name = "";
                    desc = "";
                    In.Heading = "CONSTRUCTIVE LISTENING";
                    In.IntroText = "The ability to respond from a space of deep listening is a skill that is fundamental to constructive conversations in business."; // In business interactions, it's easy to listen through the filters of our own beliefs, judgements and opinions, thus missing out on a new perspective or an opportunity to deepen our understanding of people and situations. The ability to truly listen is an empathic skill that builds and strengthens relationships between colleagues and customers.";
                    In.Url_Text = "Texts for business edge - Constructive listening.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Listening Breath.mp4";
                    break;
                case 29:
                    icon = "BusinessEdge_FeelingOverwhelmed.png";
                    name = "";
                    desc = "";
                    In.Heading = "FEEING OVERWHELMED";
                    In.IntroText = "A common feeling that is experienced in everyday business life is a sense of being overwhelmed, of drowning in deadlines, deliverables, emails and human interactions. The breath responds to this low-grade sense of panic by becoming jagged, erratic and stuck, which exacerbates the feeling.";
                    In.Url_Text = "Texts for business edge - Feeling overwhelmed.html";
                    In.Url_Aud = "Breathwork_Overwelmed.mp3";
                    In.Url_Vid = "";
                    break;

                //heart Intelligence
  
                case 31:
                    icon = "HeartIntelligence_FeelingOverwhelmed.png";
                    name = "";
                    desc = "";
                    In.Heading = "FEELING OVERWHELMED";
                    In.IntroText = "The demands of our modern lives causes many of us feel overwhelmed most of the time. The to do list keeps growing, the expectations and demands placed on us by work, family and mostly the expectations we have of ourselves mean that we rarely feel “on top of our game’.";
                    In.Url_Text = "Heart Intelligence - Feeling Overwhelmed.html";
                    In.Url_Aud = "Breathwork_Overwelmed.mp3";
                    In.Url_Vid = "";
                    break;

                case 32:
                    icon = "HeartIntelligence_Anxiety.png";
                    name = "";
                    desc = "";
                    In.Heading = "ANXIETY";
                    In.IntroText = "Anxiety is a normal natural part of the human experience. In many cases, the anxiety is natural and appropriate for the situation. We can use it to harness the adrenalized energy and to take action. ";
                    In.Url_Text = "Heart Intelligence - Anxiety.html";
                    In.Url_Aud = "Breathwork_Anxiety.mp3";
                    In.Url_Vid = "Anxiety.mp4";
                    break;

                case 33:
                    icon = "HeartIntelligence_Anger.png";
                    name = "";
                    desc = "";
                    In.Heading = "ANGER";
                    In.IntroText = "We normally react to anger in two ways, either by expressing it or by suppressing it. There is another option: to integrate it and transform it. Becoming aware of anger and releasing the energy of it through the breath prevents potentially harmful physiological consequences and effects.";
                    In.Url_Text = "Heart Intelligence - Anger.html";
                    In.Url_Aud = "Breathwork_Anger.mp3";
                    In.Url_Vid = "Segment 43.mp4";
                    break;

                case 34:
                    icon = "HeartIntelligence_Depression.png";
                    name = "";
                    desc = "";
                    In.Heading = "DEPRESSION";
                    In.IntroText = "Depression, like any other feeling has many forms and shades. It could be part of a ‘Dark night of the Soul’ event, or part of an important existential experience or situation: for example, when we experience a loss. In some cases, it is valuable and necessary to connect with the feeling of sadness and emptiness in order for an awareness or insight to arise.";
                    In.Url_Text = "Heart Intelligence - Depression.html";
                    In.Url_Aud = "Breathwork_Depression.mp3";
                    In.Url_Vid = "Anti Depression.mp4";
                    break;

                case 35:
                    icon = "HeartIntelligence_Grief.png";
                    name = "";
                    desc = "";
                    In.Heading = "GRIEF";
                    In.IntroText = "Grief will inevitably visit all of us at some point. And no matter what form the grief takes: the loss of a loved one, tragic or peaceful, sudden or expected… The void it leaves, no pill can take away. There is no formula for grief. It is a natural human experience and has to be accepted and felt.";
                    In.Url_Text = "Heart Intelligence - Grief.html";
                    In.Url_Aud = "Breathwork_Grief.mp3";
                    In.Url_Vid = "Breathing For Grief.mp4";
                    break;

                case 36:
                    icon = "HeartIntelligence_FeelingAlone.png";
                    name = "";
                    desc = "";
                    In.Heading = "FEELING ALONE";
                    In.IntroText = "Being alone can be a potentially beautiful and transformative experience. It is an opportunity to silence the mind, open the heart and tap into the wholeness and fullness of our natural state. The pain of loneliness is simply the pain of being disconnected from our essence and source."; //When we deepen our relationship with our breath, we find that is a way back home, a river of energy that carries us back to our real true self.";
                    In.Url_Text = "Heart Intelligence - Feeling Alone.html";
                    In.Url_Aud = "Breathwork_Feeling Alone.mp3";
                    In.Url_Vid = "";
                    break;

                //Go for Gold

                case 41:
                    icon = "GoforGold_TwoPhaseBreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "2 PHASE BREATHING";
                    In.IntroText = "This is an advanced technique meant to develop your breathing awareness, control, and capacity, and to get a better sense of the natural forces and dynamics at work when you breathe.";
                    In.Url_Text = "Go for gold - 2 Phase Breathing.html";
                    In.Url_Aud = "Breathwork_Two Phase.mp3";
                    In.Url_Vid = "";
                    break;
                case 42:
                    icon = "GoforGold_BetweenSets.png";
                    name = "";
                    desc = "";
                    In.Heading = "BETWEEN SETS";
                    In.IntroText = "Master the art of recovery and replenishing your energy on every level between sets and between training sessions. The new paradigm in peak performance is to master the art of recovery—and more than that, to keep up with the energy demand through good breathwork."; //As Navy SEAL Commander and author of “Unbeatable mind” says: “It is easier to keep up than it is to catch up.”";
                    In.Url_Text = "Go for gold - Between Sets.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 86.mp4";
                    break;
                case 43:
                    icon = "GoforGold_BreathHolding.png";
                    name = "";
                    desc = "";
                    In.Heading = "BREATH HOLDING";
                    In.IntroText = "A healthy person, sitting at rest, should be able to tolerate a healthy pause of a bout 30seconds after the exhale. There should be no urgent need to inhale. Someone interested in peak athletic performance should train to tolerate a controlled pause of at least 45 - 60 seconds.";
                    In.Url_Text = "Go for gold - Breath Hold Training.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Breath Holding Level 1.mp4";
                    break;
                case 44:
                    icon = "GoforGold_Leadingwiththebreath.png";
                    name = "";
                    desc = "";
                    In.Heading = "LEADING WITH BREATH";
                    In.IntroText = "‘Leading with the breath’ is a principle found in the Russian martial arts and was also taught by Ilsa MIddendorf, the famous movement and dance teacher. The basic principle is that any movement is led by the breath. This could be a physical movement, or any action or decision.";
                    In.Url_Text = "Go for gold - Leading with the Breath.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Leading with the breath.mp4";
                    break;
                case 45:
                    icon = "GoforGold_PreparationforPeakPerformance.png";
                    name = "";
                    desc = "";
                    In.Heading = "PEAK PERFORMANCE";
                    In.IntroText = "Practice these techniques to calm, focus and energize yourself, and to get into the zone before a competition, a presentation, or any challenge to. This is also a great technique to use to prepare for an important day.";
                    In.Url_Text = "Go for gold - Preparation for Peak Performance.html";
                    In.Url_Aud = "Breathwork_Peak Performance.mp3";
                    In.Url_Vid = "Preperation breath.mp4";
                    break;
                case 46:
                    icon = "GoforGold_ReverseRespiration.png";
                    name = "";
                    desc = "";
                    In.Heading = "REVERSE RESPIRATION";
                    In.IntroText = "This technique comes from Taoist warrior and the Chi Kung tradition. It is also known as paradoxical breathing. By the way it is also used for weight loss, core strength training, and various intestinal or digestive problems.";
                    In.Url_Text = "Go for gold - Reverse Respiration.html";
                    In.Url_Aud = "Breathwork_Reverse_Respiration.mp3";
                    In.Url_Vid = "Reverse Respiration Paradoxical Breathing.mp4";
                    break;
                case 47:
                    icon = "GoforGold_TeamBreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "TEAM BREATHING";
                    In.IntroText = "Breath is the most powerful way we have to actually connect to each other. This breath helps us to connect to the collective energy of the group or team. For example, when all of us shout or sing or pray out loud together, our breathing is perfectly coordinated.";
                    In.Url_Text = "Go for gold - Team Breathing.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Team breathing.mp4";
                    break;
                case 48:
                    icon = "GoforGold_WimHofMethod.png";
                    name = "";
                    desc = "";
                    In.Heading = "WIM HOF METHOD";
                    In.IntroText = "Wim Hoff is a western yogi and extreme athlete. He is known as the “Iceman” and he holds almost two dozen world records. This technique has been tested under strict medical laboratory conditions and is very powerful.";
                    In.Url_Text = "Go for gold - Wim Hoff breathing.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "The Wim Hoff Method.mp4";
                    break;


                //Be your best

                case 51:
                    icon = "BeYourbest_artofmanifestation.png";
                    name = "";
                    desc = "";
                    In.Heading = "ART OF MANIFESTATION";
                    In.IntroText = "Every thought is energy, every feeling is energy. When you align all your thoughts and feelings, your senses, emotions, posture and breathing, then all aspects of your being speak the same language, and you radiate a very high frequency of energy out into the world. This is the art of manifestation.";
                    In.Url_Text = "Be your best - Art of Manifestation.html";
                    In.Url_Aud = "Breathwork_Breathing_Sandwich.mp3";
                    In.Url_Vid = "The Breathing Sandwhich.mp4";
                    break;
                case 52:
                    icon = "BeYourbest_Confidence.png";
                    name = "";
                    desc = "";
                    In.Heading = "CONFIDENCE";
                    In.IntroText = "We have 7 main energy centers or chakras in the body that correspond to certain psycho emotional states. They are basically bridges that connect mind, body and soul. The energy center that is the seat of personal power and confidence resides in the solar plexus, located between the bottom of the ribcage and belly button."; //The corresponding color yellow.";
                    In.Url_Text = "Be your best - Confidence.html";
                    In.Url_Aud = "Breathwork_Confidence.mp3";
                    In.Url_Vid = "Breathing for confidence.mp4";
                    break;
                case 53:
                    icon = "BeYourbest_Connectingtoselfandothers.png";
                    name = "";
                    desc = "";
                    In.Heading = "CONNECTING BREATH";
                    In.IntroText = "We are all connected. We all share the same breath. All that is missing is our conscious awareness of the connection. This exercise is about using the breath to create an unmistakable and unarguable sense of this connection, to awaken a living experience of this sacred connection.";
                    In.Url_Text = "Be your best - Connecting to self and others.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Connecting Breath.mp4";
                    break;
                case 54:
                    icon = "BeYourbest_FeelingAmazing.png";
                    name = "";
                    desc = "";
                    In.Heading = "FEELING AMAZING";
                    In.IntroText = "This is a great exercise to practice when you are feeling a little low and would like to shift your state very quickly. The body can have the same biological response to a real experience as it does to an imagined one."; //For example, when you replay a negative event or experience in your mind, the body will produce the same chemicals and hormones as it did during the actual experience or event.";
                    In.Url_Text = "Be your best - Feeling amazing.html";
                    In.Url_Aud = "Breathwork_Feeling Amazing.mp3";
                    In.Url_Vid = "Feel Amazing.mp4";
                    break;
                case 55:
                    icon = "BeYourbest_FocusandAttention.png";
                    name = "";
                    desc = "";
                    In.Heading = "FOCUS AND ATTENTION";
                    In.IntroText = "“Where attention goes, energy flows.” Mastering your attention and focus is a core aspect of managing your energy, and in fact, every aspect of your life. This exercise sharpens your ability to focus or shift your attention at will, and to take charge of your energy and awareness.";
                    In.Url_Text = "Be your best - Focus and Attention.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 61.mp4";
                    break;
                case 56:
                    icon = "BeYourbest_FountainBreath.png";
                    name = "";
                    desc = "";
                    In.Heading = "FOUNTAIN BREATH";
                    In.IntroText = "Breathe long slow breaths, and as you do, imagine drawing energy through your feet, up your legs and up into your body. Draw the energy all the way up and out the top of your head, and then let it shower down around you like a fountain.";
                    In.Url_Text = "Be your best - Fountain breath.html";
                    In.Url_Aud = "Breathwork_Fountain_Breath.mp3";
                    In.Url_Vid = "Fountain breath.mp4";
                    break;
                case 57:
                    icon = "BeYourbest_Listeningfrontheheart.png";
                    name = "";
                    desc = "";
                    In.Heading = "LISTEN FROM THE HEART";
                    In.IntroText = "Deliberately get out of your head and bring your attention to your body. Get a sense of your body from head to toe. Get a sense of every cell in your body all at once. Then allow your focus to settle into your heart area, and as you breathe in concentrate the breath there. ";
                    In.Url_Text = "Be your best - Listening from the Heart.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Listening Breath.mp4";
                    break;
                case 58:
                    icon = "BeYourbest_YesBreath.png";
                    name = "";
                    desc = "";
                    In.Heading = "YES BREATH";
                    In.IntroText = "There’s a beautiful mindfulness practice called the “yes” meditation, where you meet whatever is rising up in your conscious awareness with a “yes”. The “yes” is an acknowledgment and an acceptance of what is and what is coming up, rather than a judgement or analysis. It is a feeling of “yes” more than the thought of it."; //There is a sense of allowing, of opening, of spaciousness.";
                    In.Url_Text = "Be your best - Yes breath.html";
                    In.Url_Aud = "Breathwork_Yes_Breath.mp3";
                    In.Url_Vid = "The Yes Breath.mp4";
                    break;


                //Making magic

                case 61:
                    icon = "MakingMagic_BreathingSandwich.png";
                    name = "";
                    desc = "";
                    In.Heading = "Breathing Sandwich";
                    In.IntroText = "This technique is very powerful as a tool for manifesting your dreams and your intentions. And it trains you in becoming more totally focused on anything. It involves layering the breath with a statement (a thought or affirmation), with visual imagery, with sounds and physical movements, thereby aligning breath with every level of your being.";
                    In.Url_Text = "Making magic - Creating Magic.html";
                    In.Url_Aud = "Breathwork_Breathing_Sandwich.mp3";
                    In.Url_Vid = "Path Intro 5- making magic.mp4";
                    break;
                case 62:
                    icon = "MakingMagic_FountainBreath.png";
                    name = "";
                    desc = "";
                    In.Heading = "FOUNTAIN BREATH";
                    In.IntroText = "This is a beautiful spiritual and creative breathing technique that supports you in drawing in and projecting the powerful energies of the earth and nature. And it is a great way to connect with the infinite power and the source of energy, rather than depending on your personal container or limited storehouse of energy.";
                    In.Url_Text = "Making magic - Fountain Breath.html";
                    In.Url_Aud = "Breathwork_Fountain_Breath.mp3";
                    In.Url_Vid = "Fountain breath.mp4";
                    break;
                case 63:
                    icon = "MakingMagic_Playingwiththechannel.png";
                    name = "";
                    desc = "";
                    In.Heading = "PLAY WITH THE CHANNELS";
                    In.IntroText = "We have two main channels through which we breathe: the nose and the mouth. And the nose has two channels of its own: the left and right nostrils. Sitting at rest, breathing normally, the unconscious habit should be nasal breathing.";
                    In.Url_Text = "Making magic - Playing with the Channel.html";
                    In.Url_Aud = "Breathwork_Playing with the channel.mp3";
                    In.Url_Vid = "Playing With The Channels.mp4";
                    break;
                case 64:
                    icon = "MakingMagic_Precisionunderpressure.png";
                    name = "";
                    desc = "";
                    In.Heading = "PRECISION UNDER PRESSURE";
                    In.IntroText = "Our default reactive way of breathing when we are concentrating or deeply immersed in a task that requires precision, for example intricate surgery, a delicate procedure, painting, writing, or playing a musical instrument is to hold the breath, which actually interferes in the natural flow of energy that we need to accomplish the task at hand in the best way we can.";
                    In.Url_Text = "Making magic - Precision under Pressure.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 80.mp4";
                    break;
                case 65:
                    icon = "MakingMagic_QuickBoostofEnergyandInspiration.png";
                    name = "";
                    desc = "";
                    In.Heading = "QUICK BOOST";
                    In.IntroText = "This technique is useful when we need a short-term boost of energy or creative inspiration in the midst of a task or busy day. It is inspired by a Sufi technique.";
                    In.Url_Text = "Making magic - Quick boost of Energy and Inspiration.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 14.mp4";
                    break;
                case 66:
                    icon = "MakingMagic_Tappingintothesourceofecstatic.png";
                    name = "";
                    desc = "";
                    In.Heading = "ECSTATIC ENERGY";
                    In.IntroText = "Tantra is the union of the divine masculine and feminine energies within us. It’s is about awakening the essential life force energy that animates all life. It is sexual energy in its purest form. ";
                    In.Url_Text = "Making magic - Tapping into the Source of Ecstatic Energy.html";
                    In.Url_Aud = "Breathwork_Tantric Breathing.mp3";
                    In.Url_Vid = "Three Waves of Ecstasy.mp4";
                    break;
                case 67:
                    icon = "MakingMagic_UnblockingCreativeEnergy.png";
                    name = "";
                    desc = "";
                    In.Heading = "UNBLOCKING CREATIVE ENERGY";
                    In.IntroText = "This is a powerful technique that can be used to unblock stuck creativity preventing us from manifesting our ideas.  It wakes up the creative force that for some reason we allowed to be shut down, either due to fear, criticism, or ennui."; //When this happens, we tend to dissociate from our body and feelings, which is the source of creative energy within us.";
                    In.Url_Text = "Making magic - Unblocking Creative Energy.html";
                    In.Url_Aud = "Breathwork_Unblocking Creative Energy.mp3";
                    In.Url_Vid = "Creativity.mp4";
                    break;

                //Helping the helper

                case 71:
                    icon = "HelpingtheHelper_Cleansingyourfield.png";
                    name = "";
                    desc = "";
                    In.Heading = "CLEANSING YOUR ENERGY";
                    In.IntroText = "“In perfect health, vital life forces flow strongly, but if your meridians are narrowed or blocked, then the chi can stagnate, and your mental, physical and spiritual health suffers for it”. This is a beautiful and effective qi gong practice."; //To circulate chi, prana of life force through your energy bodies, to energize your subtle energy channels and move out any stagnant energy that has accumulated in your energy fields and preventing them from manifesting physically.";
                    In.Url_Text = "Helping the helper - Cleansing your Field.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Path Intro 6- helping the helper.mp4";
                    break;
                case 72:
                    icon = "HelpingtheHelper_Compassionthroughconnecting.png";
                    name = "";
                    desc = "";
                    In.Heading = "COMPASSION";
                    In.IntroText = "“Separation is an optical delusion of consciousness” - Albert Einstein. When you work in the helping and healing professions it’s easy to become overwhelmed by the suffering of others, to take on their problems to a certain extent, and as a result suffer emotional drained or burned out."; //We often experience what we call “empathy fatigue.”";
                    In.Url_Text = "Helping the helper - Compassion through Connection.html";
                    In.Url_Aud = "Breathwork_Compassion.mp3";
                    In.Url_Vid = "Breath Of Compassion.mp4";
                    break;
                case 73:
                    icon = "HelpingtheHelper_Listeningwithlove.png";
                    name = "";
                    desc = "";
                    In.Heading = "LISTEN WITH LOVE";
                    In.IntroText = "“Being listened to is so close to being loved that for the average person, they are almost indistinguishable.”  - David Augsburger. More powerful than any potent medicine, procedure or healing modality is the feeling of being truly seen and heard."; //To be truly heard requires that the listener has a certain quality of presence to feel into the spaces between words, to drop into the essence of what is being shared, and to take in the energy behind what is being said.";
                    In.Url_Text = "Helping the helper - Listening with Love.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Segment 24.mp4";
                    break;
                case 74:
                    icon = "HelpingtheHelperr_LovingKindness.png";
                    name = "";
                    desc = "";
                    In.Heading = "LOVING KINDNESS";
                    In.IntroText = "Deepening our compassion for others begins with the practice of extending or feeling compassion for ourselves. The heart is the seat of compassion and conscious breathing connects us to our heart. It works to dissolve any thoughts or feelings that separate us from each other. When we breathe, we belong."; //The sensation of the breath in our bodies is the ultimate experience of being loved by existence itself.";
                    In.Url_Text = "Helping the helper - Loving Kindness.html";
                    In.Url_Aud = "Breathwork_Loving_Kindness_Compassion.mp3";
                    In.Url_Vid = "";
                    break;
                case 75:
                    icon = "HelpingtheHelper_Shakingitout.png";
                    name = "";
                    desc = "";
                    In.Heading = "SHAKING IT OUT";
                    In.IntroText = "This is a quick and effective technique that can be used as a quick fix release of toxic or negative energy that you you’ve absorbed, for example after a healing session.";
                    In.Url_Text = "Helping the helper - Shaking It Out.html";
                    In.Url_Aud = "";
                    In.Url_Vid = "Shaking breath.mp4";
                    break;
                case 76:
                    icon = "HelpingtheHelper_SpiritualVacuumCleaner.png";
                    name = "";
                    desc = "";
                    In.Heading = "SPIRITUAL VACUUM CLEANER";
                    In.IntroText = "This is an advanced meditation using the heart as a transformer of energy.";
                    In.Url_Text = "Helping the helper - Spiritual Vacuum Cleaner.html";
                    In.Url_Aud = "Breathwork_Spiritual Vacuum Cleaner.mp3";
                    In.Url_Vid = "Spiritual Vacuum Cleaner (Spelling mistake).mp4";
                    break;

                //Soul Source
                case 81:
                    icon = "SoulSorce_ConsciousConnectedBreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "CONSCIOUS CONNECTED BREATH";
                    In.IntroText = "This technique stems from the original Rebirthing technique brought to the west by Leonard Orr. The Conscious Connected rhythm or rebirthing technique is likely the most powerful transformational breathing technique on the planet.";
                    In.Url_Text = "Soul source - Conscious Connected Breathing.html";
                    In.Url_Aud = "Breathwork_20 Connected Breathes.mp3";
                    In.Url_Vid = "Connected Breath.mp4";
                    break;
                case 82:
                    icon = "SoulSorce_CosmicBreaths.png";
                    name = "";
                    desc = "";
                    In.Heading = "COSMIC BREATH";
                    In.IntroText = "Lie on your back and get really relaxed. Let your borders, your boundaries dissolve. In your mind’s eye, imagine yourself as a star floating in the cosmos.";
                    In.Url_Text = "Soul source - Cosmic Breaths.html";
                    In.Url_Aud = "Breathwork_Cosmic Breathes.mp3";
                    In.Url_Vid = "Cosmic Breath.mp4";
                    break;
                case 83:
                    icon = "SoulSorce_DereflexiveBreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "DE-REFLEXIVE BREATH";
                    In.IntroText = "Everything in existence has consciousness, and when we get too fixated on our own individual consciousness, we get out of balance with the rest of the universe. This technique is also called Krishna’s Kriya Yoga. It is a very beautiful meditation and spiritual breathing technique."; //It supports our awakening to Oneness and Wholeness, and to our connection with others.";
                    In.Url_Text = "Soul source - De-Reflexive Breathing.html";
                    In.Url_Aud = "Breathwork_Krishna Kriya.mp3";
                    In.Url_Vid = "De-Reflexive Breathing Krishnas Kriya.mp4";
                    break;
                case 84:
                    icon = "SoulSorce_MicrocosmicOrbit.png";
                    name = "";
                    desc = "";
                    In.Heading = "MICRO COSMIC BREATH";
                    In.IntroText = "In perfect health, your vital life force flows strongly, but if your meridians are narrowed or blocked, then the chi can stagnate, and your mental, physical and spiritual health suffers for it”. This practice was popularized in the west by Mantak Chia. It is a beautiful and effective qigong exercise. In China it is called the lesser respiratory cycle.";
                    In.Url_Text = "Soul source - Micro Cosmic Orbit.html";
                    In.Url_Aud = "Breathwork_MicroCosmic.mp3";
                    In.Url_Vid = "Micro Cosmic Orbit.mp4";
                    break;
                case 85:
                    icon = "SoulSorce_PrayerBreath.png";
                    name = "";
                    desc = "";
                    In.Heading = "PRAYER BREATH";
                    In.IntroText = "This exercise is inspired by Ram Dass, a Harvard professor by the name of Richard Alpert, who became one of America’s most loved spiritual teachers. It is very simple.";
                    In.Url_Text = "Soul source - Prayer Breath.html";
                    In.Url_Aud = "Breathwork_Prayer Breathe.mp3";
                    In.Url_Vid = "Prayer breath.mp4";
                    break;
                case 86:
                    icon = "SoulSorce_RelaxedSubtleenergybreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "RELAXED SUBTLE ENERGY";
                    In.IntroText = "In this technique, you are relaxing so much and breathing so softly and subtly that it is almost imperceptible from the outside. However, on the inside it is very rich and very bright. The breath is so subtle and relaxed that If you had to place a candle in front of your nose, the flame would not flicker. The focus is on the feeling of energy not the air.";
                    In.Url_Text = "Soul source - Relaxed Subtle Energy Breathing.html";
                    In.Url_Aud = "Breathwork_Relaxed Subtle Energy.mp3";
                    In.Url_Vid = "Relaxed Subtle Energy Breathing.mp4";
                    break;
                case 87:
                    icon = "SoulSorce_Threewavesofbreathing.png";
                    name = "";
                    desc = "";
                    In.Heading = "THREE WAVES OF BREATHING";
                    In.IntroText = "There is great joy when we send positive heartfelt loving intentions out into the world, like a flower releasing its fragrance. This spiritual breathing exercise is similar to something that many Buddhist monks practice: each day, facing the four directions of the compass and sending peace and compassion to everyone in the world";
                    In.Url_Text = "Soul source - Three Waves of Energy.html";
                    In.Url_Aud = "Breathwork_3 Waves of Energy.mp3";
                    In.Url_Vid = "Segment 36.mp4";
                    break;
            }



            await Navigation.PushAsync(new IntemediaryView(In.Heading, In.IntroText, icon, name, desc, In.Url_Text, In.Url_Aud, In.Url_Vid));

            ((ListView)sender).SelectedItem = null;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    base.OnBackButtonPressed();
        //    // GoBack();
        //    return true;
        //}

        private async void Close_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
        private async void GoBack()
        {
            await Navigation.PushAsync(new CoreMenuTemplate("FindYourPath"));
        }
        private async void Search_Pressed(object sender, EventArgs e)
        {
            searchBar.IsVisible = true;
            //search.IsVisible = false;
            //await PopupNavigation.PushAsync(new SearchPopup());
        }
    }
}