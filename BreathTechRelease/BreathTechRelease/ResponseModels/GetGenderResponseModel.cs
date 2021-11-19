using System;
using System.Collections.Generic;
namespace BreathTechRelease.ResponseModels
{
    public class GetGenderResponseModel
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public List<LstGender> lstGender { get; set; }
    }
    public class LstGender
    {
        public int id { get; set; }
        public string name { get; set; }

    }
}
