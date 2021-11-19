using System;
namespace BreathTechRelease.RequestModels
{
    public class SupportHistoryRequestModel
    {
        public string toEmail { get; set; }
        public string userEmail { get; set; }
        public string name { get; set; }
        public string lName { get; set; }
        public string contactNo { get; set; }
        public string message { get; set; }
    }
}
