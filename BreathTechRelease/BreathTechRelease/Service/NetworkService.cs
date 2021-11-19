using System;
using Refit;

namespace BreathTechRelease.Service
{
    public static class NetworkService
    {
        public static IBreathTechAPI apiService;
        //UAT
        //public static string baseUrl = "http://154.0.166.250/BreathTechAPI_UAT/";

        //Prod
        //public static string baseUrl = "https://payments.breathtechapp.com/";

        public static string baseUrl = "https://app.breathtechapp.com/BTAPI";
        public static string htmlContentsUrl = "https://app.breathtechapp.com/BTAPI/htmlcontents";
        public static string mediaBaseUrl = "https://app.breathtechapp.com/testmedia";
        public static string newsBaseUrl = "https://breathtechapp.com/news/";
        public static string Key = "b14ca5898a4e4133bbce2ea2315a1916";

        public static IBreathTechAPI GetApiService()
        {
            apiService = RestService.For<IBreathTechAPI>(baseUrl);
            return apiService;
        }
    }
}