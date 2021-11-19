using BreathTechRelease.ViewModels;
using BreathTechRelease.Views;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BreathTechRelease.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryPopUp : PopupPage
    {
        private Registration registration;
        private UpdateProfileView updateProfileView;

        public CountryPopUp(Registration registration)
        {
            InitializeComponent();
            this.BindingContext = new CountryPopupViewModel(registration);
        }

        public CountryPopUp(UpdateProfileView updateProfileView)
        {
            InitializeComponent();
            this.BindingContext = new CountryPopupViewModel(updateProfileView);
        }
    }
}