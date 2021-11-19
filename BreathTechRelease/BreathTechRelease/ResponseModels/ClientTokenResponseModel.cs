using System;
namespace BreathTechRelease.ResponseModels
{
    public class ClientTokenResponseModel
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string clientToken { get; set; }
    }
}
