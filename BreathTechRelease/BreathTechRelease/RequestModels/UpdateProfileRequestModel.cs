using System;
namespace BreathTechRelease.RequestModels
{
    public class UpdateProfileRequestModel
    {
        public int recordID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mobile { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }
        public Guid appid { get; set; }
        public string deviceid { get; set; }
        public DateTime sysDate { get; set; }
        public bool isVerifyEmail { get; set; }
        public string model { get; set; }
        public string dType { get; set; }
        public string oVersion { get; set; }
        public string dPlatform { get; set; }
        public string didiom { get; set; }
        public string customerId { get; set; }
        public int countryId { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int genderId { get; set; }
        //public string profilePic { get; set; }
        //public string formFile { get; set; }
    }
}
