using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.RequestModels
{
    public class GoogleLoginRequestModel
    {
        [JsonProperty("googleToken")]
        public string GoogleToken { get; set; }

        [JsonProperty("googleId")]
        public string GoogleId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("firstName")]
        public string FisrtName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

    }
}
