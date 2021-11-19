using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace BreathTechRelease.Models
{

    [JsonObject]
    public class User
    {

        [JsonProperty("id")]
        [PrimaryKey]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public string VerifiedEmail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        public static readonly User Default = new User()
        {
            Id = "NA",
            Name = "Unknown",
            Gender = "Unknown",
            Picture = "Not Set",
            Link = "Unknown",
            FamilyName = "Unknown",
            GivenName = "Unknown",
            VerifiedEmail = "false",
            Email = "Unknown",
        };

    }
}
