using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.ResponseModels
{
    public class ValidatePromoCodeResponseModel
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
        public int promoCodeId { get; set; }
        public double subscriptionAmount { get; set; }
        public double discountedAmount { get; set; }
        public double payableAmount { get; set; }
    }
}
