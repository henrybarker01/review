using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.ResponseModels
{
    public class User
    {
        public int recordID { get; set; }
        public string uid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string customerId { get; set; }
        public bool isSubcriptionActive { get; set; }
        public DateTime subscriptionEndDate { get; set; }
        public int roleID { get; set; }
        public int subscriptionTypeId { get; set; }
        public string subscriptionType { get; set; }
        public string mobile { get; set; }
        public int countryId { get; set; }
        public string countryFlag { get; set; }
        public bool isAutoSubscriptionActivated { get; set; }
        public DateTime subscriptionAutoActivationDate { get; set; }
        public DateTime subscriptionAutoDeActivationDate { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int genderId { get; set; }
        public string genderName { get; set; }

    }
    public class GoogleLoginResponseModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public User userDetails { get; set; }
        public string token { get; set; }

    }
    
}
