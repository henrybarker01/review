using System;
namespace BreathTechRelease.RequestModels
{
    public class InsertTransactionRequestModel
    {
        public int transactionId { get; set; }
        public string referenceId { get; set; }
        public string payRequestId { get; set; }
        public string transactionDate { get; set; }
        public string amount { get; set; }
        public string transactionStatus { get; set; }
        public string resultCode { get; set; }
        public string resultDescription { get; set; }
        public string customerEmail { get; set; }
        public string uid { get; set; }
        public string subscriptionId { get; set; }
        public string subscriptionName { get; set; }
        public string subscriptionEndDate { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string vaultId { get; set; }
        public int promoCodeId { get; set; }
        public string promoCode { get; set; }
        public string promoCodeAmount { get; set; }
        public string finalAmount { get; set; }
        public string applePurchaseId { get; set; }
        public string applePurchaseToken { get; set; }
        public string applePurchaseState { get; set; }
        public bool isAppleTransaction { get; set; }
    }
}
