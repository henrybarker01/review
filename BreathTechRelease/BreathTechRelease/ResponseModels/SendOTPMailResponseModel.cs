using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.ResponseModels
{
    public class SendOTPMailResponseModel
    {
        public bool isSuccess { get; set; }
        public string outMSG { get; set; }
    }
}
