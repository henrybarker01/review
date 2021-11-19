using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BreathTechRelease.RequestModels;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Service;
using Newtonsoft.Json;
using Acr.UserDialogs;
using System.Collections.Generic;
using BreathTechRelease.Models;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace BreathTechRelease.Manager
{
    public class BreathtechAPIManager
    {
        public static async Task<LoginResponseModel> Login(LoginRequestModel model)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.Login(serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<LoginResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<LoginResponseModel>(s);
                    return JsonConvert.DeserializeObject<LoginResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<RegisterResponseModel> Register(RegisterUserRequestModel model)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.Register(serializedmodel);
                if (data != null)
                {

                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<RegisterResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<RegisterResponseModel>(s);
                    return JsonConvert.DeserializeObject<RegisterResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<PaymentResponseModel> MakePayment(string authToken, string customerId, double amount, string nonce, string payment_channel, string subscriptionid)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                PaymentRequestModel model = new PaymentRequestModel();
                model.customerId = customerId;
                model.amount = amount;
                model.paymentNonce = nonce;
                model.paymentChannel = payment_channel;
                model.subscriptionTypeId = subscriptionid;
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.MakePayment(authToken, serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<PaymentResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<PaymentResponseModel>(s);
                    return JsonConvert.DeserializeObject<PaymentResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<ClientTokenResponseModel> GetClientToken(string authToken, string customerId)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.GetClientToken(authToken, customerId);
                if (data != null)
                {

                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ClientTokenResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ClientTokenResponseModel>(s);
                    return JsonConvert.DeserializeObject<ClientTokenResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<SubscriptionTypeListResponseModel> GetSubscriptionTypes(string authToken)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.GetSubscription(authToken);
                if (data != null)

                {

                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<SubscriptionTypeListResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<SubscriptionTypeListResponseModel>(s);
                    return JsonConvert.DeserializeObject<SubscriptionTypeListResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<GoogleLoginResponseModel> GoogleLogin(GoogleLoginRequestModel model)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.GoogleLogin(serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<GoogleLoginResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<GoogleLoginResponseModel>(s);
                    return JsonConvert.DeserializeObject<GoogleLoginResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<ResendVerificationMailResponseModel> ResendVerificationMail(string emailId)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                var data = await apiService.ResendVerificationEmail(emailId);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ResendVerificationMailResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ResendVerificationMailResponseModel>(s);
                    return JsonConvert.DeserializeObject<ResendVerificationMailResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<ResetPasswordResponseModel> ResetPassword(string Email,string Password)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                var data = await apiService.ResetPassword(Email,Password);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ResetPasswordResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ResetPasswordResponseModel>(s);
                    return JsonConvert.DeserializeObject<ResetPasswordResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<ChangePasswordResponseModel> ChangePassword(string token, string Email, string currentPassword, string newPassword)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                var data = await apiService.ChangePassword(token, Email, currentPassword, newPassword);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ChangePasswordResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ChangePasswordResponseModel>(s);
                    return JsonConvert.DeserializeObject<ChangePasswordResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<SendToSupportResponseModel> SendToSupport(string authToken,string userEmail, SupportHistoryRequestModel supportHistoryRequestModel)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.SendToSupport(authToken,supportHistoryRequestModel.toEmail,userEmail,supportHistoryRequestModel.name,supportHistoryRequestModel.message);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<SendToSupportResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<SendToSupportResponseModel>(s);
                    return JsonConvert.DeserializeObject<SendToSupportResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<SendOTPMailResponseModel> SendOTPMail(string Email)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                var data = await apiService.SendOTPMail(Email);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<SendOTPMailResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<SendOTPMailResponseModel>(s);
                    return JsonConvert.DeserializeObject<SendOTPMailResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<ValidateOTPResponseModel> ValidateOTP(string email, string Otp)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.ValidateOTP(email, Otp);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ValidateOTPResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ValidateOTPResponseModel>(s);
                    return JsonConvert.DeserializeObject<ValidateOTPResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<CountryDataResponseModel> GetAllCountryData()
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.GetAllCountryData();
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<CountryDataResponseModel>();
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<CountryDataResponseModel>(s);
                    return JsonConvert.DeserializeObject<CountryDataResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<GetUserDetailByIdResponseModel> GetUserDetailById(string authToken, int recordId)
        {
            try
            {
                //var json = JsonConvert.SerializeObject(updateCustomerProfileReqModel);
                
                var client = new RestClient("https://app.breathtechapp.com/BTAPI/User/GetUserDetailById?RecordId=" + recordId);
                
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", authToken);
                IRestResponse response =await client.ExecuteAsync(request);
                
                var ResultData = JsonConvert.DeserializeObject<GetUserDetailByIdResponseModel>(response.Content);
                //var apiService = NetworkService.GetApiService();
                //var data = await apiService.GetUserDetailById(authToken, recordId);
                //var data = await apiService.GetUserDetailById(authToken, recordId);
                if (ResultData != null)
                {
                    return ResultData;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<GetUserDetailByIdResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<GetUserDetailByIdResponseModel> (s);
                    return JsonConvert.DeserializeObject<GetUserDetailByIdResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<UpdateProfileResponseModel> UpdateProfile(string auth_token,UpdateProfileRequestModel model)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.UpdateProfile(auth_token,serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<UpdateProfileResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<UpdateProfileResponseModel>(s);
                    return JsonConvert.DeserializeObject<UpdateProfileResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<ValidatePromoCodeResponseModel> ValidatePromocodeForUser(string auth_token, int recordId, string promoCodeEntry,int subscriptionTypeId)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.ValidatePromocodeForUser(auth_token, recordId, promoCodeEntry,subscriptionTypeId);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<ValidatePromoCodeResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<ValidatePromoCodeResponseModel>(s);
                    return JsonConvert.DeserializeObject<ValidatePromoCodeResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<GetGenderResponseModel> GetAllGender()
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.GetAllGender();
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<GetGenderResponseModel>();
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<GetGenderResponseModel>(s);
                    return JsonConvert.DeserializeObject<GetGenderResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<InsertUpdateMatriceResponseModel> InsertUpdateMatricesBulk(string auth_token, List<InsertUpdateMatriceRequestModel> model)
        {

            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.InsertUpdateMatricesBulk(auth_token, serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<InsertUpdateMatriceResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<InsertUpdateMatriceResponseModel>(s);
                    return JsonConvert.DeserializeObject<InsertUpdateMatriceResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<GoogleLoginResponseModel> AppleLogin(AppleLoginRequestModel model)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.AppleLogin(serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<GoogleLoginResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<GoogleLoginResponseModel>(s);
                    return JsonConvert.DeserializeObject<GoogleLoginResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<InsertTransactionResponseModel> InsertTransactionApple(string auth_token, InsertTransactionRequestModel model)
        {
            try
            {
                var apiService = NetworkService.GetApiService();
                string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.InsertTransactionApple(auth_token,serializedmodel);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<InsertTransactionResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<InsertTransactionResponseModel>(s);
                    return JsonConvert.DeserializeObject<InsertTransactionResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<VideoPlayedResponseModel> VideoPlayed(int recordId, bool isVideoPlayed)
        {

            //var apiService = NetworkService.GetApiService();
            //var data = await apiService.GetUserDetailById(authToken, recordId);

            try
            {
                var apiService = NetworkService.GetApiService();
                //string serializedmodel = JsonConvert.SerializeObject(model);
                var data = await apiService.UpdateVideoPlayed(recordId, isVideoPlayed);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Refit.ApiException ex)
            {
                var content = ex.GetContentAs<LoginResponseModel>();
                Debug.WriteLine(ex.Message);
                var s = ex.Content;
                if (s != null)
                {
                    var data = JsonConvert.DeserializeObject<VideoPlayedResponseModel>(s);
                    return JsonConvert.DeserializeObject<VideoPlayedResponseModel>(s);
                }
                else
                {
                    Console.WriteLine("Error-----------");
                    Console.WriteLine(@" Error {0}", ex.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            //try
            //{
            //    var apiService = NetworkService.GetApiService();
            //    string serializedmodel = JsonConvert.SerializeObject(model);
            //    var data = await apiService.Login(serializedmodel);
            //    if (data != null)
            //    {
            //        return data;
            //    }
            //    return null;
            //}
            //catch (Refit.ApiException ex)
            //{
            //    var content = ex.GetContentAs<LoginResponseModel>();
            //    Debug.WriteLine(ex.Message);
            //    var s = ex.Content;
            //    if (s != null)
            //    {
            //        var data = JsonConvert.DeserializeObject<LoginResponseModel>(s);
            //        return JsonConvert.DeserializeObject<LoginResponseModel>(s);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error-----------");
            //        Console.WriteLine(@" Error {0}", ex.Message);
            //        return null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
    }
}
