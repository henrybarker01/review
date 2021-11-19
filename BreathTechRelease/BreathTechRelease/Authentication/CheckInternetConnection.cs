using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BreathTechRelease.Authentication
{
    public class CheckInternetConnection
    {
        public static async Task<bool> CheckConnection()
        {
            try
            {
                return Connectivity.NetworkAccess == NetworkAccess.Internet;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
