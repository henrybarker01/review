using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetTheTeam : ContentPage
    {
        public MeetTheTeam()
        {
            InitializeComponent();
        }

        private async void CLOSE_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}