using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BreathTechRelease.Models
{

    public class OsmosysRestAPI
    {

        public string INSBTUGUsers(string strFname
           , string strLname
           , string strMobile
           , string strUsername
           , string strPassword
           , string streMail
           , string strAPPID
           , string strDEVICEID
           , string strVerifyEmail
           , string strModel
           , string strDType
           , string strOVersion
           , string strDPlatform
           , string strDIDIOM
           , int intMode)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var client = new RestClient("https://incapdev.thedream.global/APIStore/api/androidauthentication.asmx");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("SOAPAction", "http://TheDream.global/INSBTUGUssers");
            request.AddHeader("Content-Type", "text/xml");
            request.AddParameter("text/xml",
                                 "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Body>\r\n    <INSBTUGUssers xmlns=\"http://TheDream.global\">\r\n      <strFname>" 
                                 + strFname + 
                                 "</strFname>\r\n      <strLname>"
                                 + strLname + 
                                 "</strLname>\r\n      <strMobile>"
                                 + strMobile + 
                                 "</strMobile>\r\n      <strUsername>" 
                                 + strUsername + 
                                 "</strUsername>\r\n      <strPassword>" 
                                 + strPassword + 
                                 "</strPassword>\r\n      <streMail>"                                 
                                 + streMail +
                                 "</streMail>\r\n      <strAPPID>" 
                                 + strAPPID + 
                                 "</strAPPID>\r\n      <strDEVICEID>"
                                 + strDEVICEID +
                                 "</strDEVICEID>\r\n      <strVerifyEmail>" 
                                 + strVerifyEmail + 
                                 "</strVerifyEmail>\r\n      <strModel>"
                                 + strModel + 
                                 "</strModel>\r\n      <strDType>"
                                 + strDType + 
                                 "</strDType>\r\n      <strOVersion>"
                                 + strOVersion +
                                 "</strOVersion>\r\n      <strDPlatform>"
                                 + strDPlatform +
                                 "</strDPlatform>\r\n      <strDIDIOM>" 
                                 + strDIDIOM + 
                                 "</strDIDIOM>\r\n      <intMode>"
                                 + intMode +
                                 "</intMode>\r\n    </INSBTUGUssers>\r\n  </soap:Body>\r\n</soap:Envelope>",
                                 ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);




            return response.Content.ToString();

        }


        public string SendMail(string strFrom, string strTO, string strSubject, string strBody)

        {

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var client = new RestClient("https://incapdev.thedream.global/APIStore/api/osmmail.asmx");

            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "text/xml");

            request.AddParameter("text/xml", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" +
                " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Body>\r\n   " +
                " <SendMail xmlns=\"http://TheDream.global\">\r\n      <strFrom>"
                + strFrom +
                "</strFrom>\r\n      <strTO>"
                + strTO + 
                "</strTO>\r\n      <strSubject>"
                + strSubject + 
                "</strSubject>\r\n      <strBody>"
                + strBody + 
                "</strBody>\r\n    </SendMail>\r\n  </soap:Body>\r\n</soap:Envelope>", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);


            return response.Content;

        }
    }
}
