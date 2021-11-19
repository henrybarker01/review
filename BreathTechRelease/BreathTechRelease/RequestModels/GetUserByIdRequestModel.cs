using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.RequestModels
{
    public class GetUserByIdRequestModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class UserData
        {
            public int RecordID { get; set; }
            public string UID { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string CustomerId { get; set; }
            public bool IsSubcriptionActive { get; set; }
            public DateTime SubscriptionEndDate { get; set; }
            public int RoleID { get; set; }
            public int SubscriptionTypeId { get; set; }
            public string SubscriptionType { get; set; }
            public string Mobile { get; set; }
            public int CountryId { get; set; }
            public string CountryFlag { get; set; }

            public bool isVideoPlayed { get; set; }
            public bool IsAutoSubscriptionActivated { get; set; }
            public DateTime SubscriptionAutoActivationDate { get; set; }
            public DateTime SubscriptionAutoDeActivationDate { get; set; }


        }

        public class Root
        {
            public bool isSuccess { get; set; }
            public string message { get; set; }
            public UserData userData { get; set; }
        }


    }
}
