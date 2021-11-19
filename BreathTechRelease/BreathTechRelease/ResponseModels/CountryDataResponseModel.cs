using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.ResponseModels
{
    public class CountryDataResponseModel
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IList<UcCountry> btCountry { get; set; }
    }
    public class UcCountry
    {
        public int countryId { get; set; }
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string countryPrefix { get; set; }
        public string countryFlag { get; set; }

    }
}
