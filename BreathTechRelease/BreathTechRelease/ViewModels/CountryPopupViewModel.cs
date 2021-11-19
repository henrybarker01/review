using BreathTechRelease.Authentication;
using BreathTechRelease.Manager;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreathTechRelease.ViewModels
{
    
    public class CountryPopupViewModel : BaseINotifyPropertyChanged
    {
        private Registration registration;
        private UpdateProfileView updateProfileView;
        public CountryPopupViewModel(Registration registration)
        {
            this.registration = registration;
            GetAllCountries();
        }

        public CountryPopupViewModel(UpdateProfileView updateProfileView)
        {
            this.updateProfileView = updateProfileView;
            GetAllCountries();
        }

        private UcCountry selectedCountry;

        public UcCountry SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;
                if (value != null)
                {
                    var country = selectedCountry as UcCountry;
                    if(registration!=null)
                    {
                        registration.CountryCode = country.countryCode;
                        registration.CountryFlag = country.countryFlag;
                        registration.CountryId = country.countryId;
                    }
                    else
                    {
                        updateProfileView.CountryCode = country.countryCode;
                        updateProfileView.CountryFlag = country.countryFlag;
                        updateProfileView.CountryId = country.countryId;
                    }
                    //UserRegisterationVM.SelectedCountry = country.countryCode;
                    //UserRegisterationVM.SelectedCountryCode = country.countryPrefix;
                    //UserRegisterationVM.SelectedCountryImage = country.countryFlag;
                    //UserRegisterationVM.CountryId = country.countryId;
                    PopupNavigation.PopAsync();
                    //App.Current.MainPage.Navigation.PushAsync(new UserRegistrationTemplate(country));
                }
                OnPropertyChanged("SelectedCountry");
            }
        }


        private string searchCountry;
        public string SearchCountry
        {
            get
            {
                return searchCountry;
            }
            set
            {
                searchCountry = value;
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    GetAllCountries();
                }
                else
                {
                    try
                    {
                        CountryList = new List<UcCountry>(CountryListCopied.Where(s => s.countryName.ToLower().Contains(value.ToLower())));
                    }
                    catch (Exception ex) { }
                }
                OnPropertyChanged("SearchCountry");
            }
        }

        private List<UcCountry> countryList;

        public List<UcCountry> CountryList
        {
            get
            {
                return countryList;
            }
            set
            {
                countryList = value;
                OnPropertyChanged("CountryList");
            }
        }

        private List<UcCountry> countryListCopied;
        public List<UcCountry> CountryListCopied
        {
            get
            {
                return countryListCopied;
            }
            set
            {
                countryListCopied = value;
                OnPropertyChanged("CountryListCopied");
            }
        }

        public async void GetAllCountries()
        {
            CountryList = new List<UcCountry>();
            try
            {
                bool isInternetConnectionEnabled = await CheckInternetConnection.CheckConnection();
                if (isInternetConnectionEnabled)
                {
                    var result = await BreathtechAPIManager.GetAllCountryData();
                    if (result != null && result.issuccess == true)
                    {
                        CountryList = result.btCountry.ToList();
                        CountryListCopied = CountryList;
                    }
                    else if (result != null && result.issuccess == false)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", result.message, "Cancel");
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new MaintenanceAlert());
                    }
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "No internet connection", "Ok");
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
    
}
