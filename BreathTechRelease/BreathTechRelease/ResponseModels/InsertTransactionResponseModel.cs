using System;
namespace BreathTechRelease.ResponseModels
{
    public class InsertTransactionResponseModel
    {
        public bool issuccess { get; set; }
        public string outMSG { get; set; }
        public int transactionId { get; set; }
    }
}
