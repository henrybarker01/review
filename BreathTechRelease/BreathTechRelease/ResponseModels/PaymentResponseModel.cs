using System;
namespace BreathTechRelease.ResponseModels
{
    public class PaymentResponseModel
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
        public string transactionStatus { get; set; }
    }
}
