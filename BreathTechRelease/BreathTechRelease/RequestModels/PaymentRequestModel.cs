using System;
namespace BreathTechRelease.RequestModels
{
    public class PaymentRequestModel
    {
        public string customerId { get; set; }
        public double amount { get; set; }
        public string paymentNonce { get; set; }
        public string paymentChannel { get; set; }
        public string subscriptionTypeId { get; set; }
    }
}
