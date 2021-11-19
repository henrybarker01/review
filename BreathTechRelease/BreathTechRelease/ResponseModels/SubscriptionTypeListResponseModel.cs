using System;
using System.Collections.Generic;

namespace BreathTechRelease.ResponseModels
{
    public class SubscriptionTypeListResponseModel
    {
        public string message { get; set; }
        public string statusCode { get; set; }
        public bool issuccess { get; set; }
        public List<SubscriptionType> subscriptionType { get; set; }
    }
    public class SubscriptionType
    {
        public int subscriptionTypeId { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
    }
}
