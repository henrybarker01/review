using System;
namespace BreathTechRelease.Models
{
    public class AppleLoginRequestModel
    {
        public string appleUserId { get; set; }
        public string email { get; set; }   
        public string fisrtName { get; set; }
        public string lastName { get; set; }
        public string appleToken { get; set; }
        public string realUserStatus { get; set; }
    }
}
