using System;
using System.Globalization;
using System.Threading.Tasks;
using BreathTechRelease.RequestModels;
using BreathTechRelease.ResponseModels;
using BreathTechRelease.Views;
using Refit;

namespace BreathTechRelease.Service
{
    public interface IBreathTechAPI
    {
        [Post("/User/userlogin")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<LoginResponseModel> Login([Body] string userdata);

        [Post("/User/UserRegistration")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<RegisterResponseModel> Register([Body] string userdata);

        [Get("/BraintreeGateway/GetClientToken?aCustomerId={customerID}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ClientTokenResponseModel> GetClientToken([Header("Authorization")] string auth_token, string customerID);

        [Post("/BraintreeGateway/TransactionRequest")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<PaymentResponseModel> MakePayment([Header("Authorization")] string auth_token, [Body] string user_data);

        [Get("/Subscription/GetAllSubscriptionType")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<SubscriptionTypeListResponseModel> GetSubscription([Header("Authorization")] string auth_token);

        [Post("/User/GoogleLogin")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GoogleLoginResponseModel> GoogleLogin([Body] string userdata);

        [Post("/User/SendVerificationLinkEmail?emailId={emailId}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ResendVerificationMailResponseModel> ResendVerificationEmail([Body] string emailId);

        [Post("/User/ResetPassword?Email={Email}&Password={Password}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ResetPasswordResponseModel> ResetPassword(string Email, string Password);

        [Post("/User/ChangePassword?Email={Email}&CurrentPassword={CurrentPassword}&NewPassword={NewPassword}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ChangePasswordResponseModel> ChangePassword([Header("Authorization")] string auth_token, string Email, string CurrentPassword, string NewPassword);

        //[Post("/User/SendToSupport?toEmail={toEmail}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<SendToSupportResponseModel> SendToSupport([Header("Authorization")] string auth_token, string toEmail, [Body]SupportHistoryRequestModel supportHistoryRequestModel);

        [Post("/User/SendToSupport?userEmail={userEmail}&toEmail={toEmail}&name={name}&message={message}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<SendToSupportResponseModel> SendToSupport([Header("Authorization")] string auth_token, string userEmail, string toEmail, string name, string message);


        [Post("/User/SendOTPEmail?EmailId={Email}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<SendOTPMailResponseModel> SendOTPMail(string Email);

        [Get("/User/ValidateOTP?EmailId={Email}&OTP={OTP}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ValidateOTPResponseModel> ValidateOTP(string Email, String OTP);


        [Get("/User/GetUserDetailById?recordId={RecordId}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<Root> GetUserDetail([Header("Authorization")] string auth_token,int RecordId);


        [Get("/User/UserRegistration")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<RegisterResponseModel> UserRegistration([Body] string userdata);

        [Get("/Country/GetAllCountry")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<CountryDataResponseModel> GetAllCountryData();

        [Get("/User/GetUserDetailById?RecordId={recordId}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GetUserDetailByIdResponseModel> GetUserDetailById([Header("Authorization")] string auth_token, int recordId);

        [Post("/User/CancelAutoSubscription")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<CancelAutoSubscription> CancelAutoSubscription([Header("Authorization")] string auth_token, string uid);


        [Post("/api/PromoCode/ValidatePromocodeForUser?userId={userId}&promoCode={promoCode}&subscriptionTypeId={subscriptionTypeId}")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<ValidatePromoCodeResponseModel> ValidatePromocodeForUser([Header("Authorization")] string auth_token, int userId, string promoCode,int subscriptionTypeId);

        [Post("/User/ProfileUpdate")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<UpdateProfileResponseModel> UpdateProfile([Header("Authorization")] string auth_token, [Body] string userdata);

        [Get("/User/GetAllGender")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GetGenderResponseModel> GetAllGender();

        [Post("/api/Matrice/InsertUpdateMatricesBulk")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<InsertUpdateMatriceResponseModel> InsertUpdateMatricesBulk([Header("Authorization")] string auth_token, [Body] string userdata);

        [Post("/User/AppleLogin")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<GoogleLoginResponseModel> AppleLogin([Body] string userdata);

        [Post("/api/Apple/InsertTransactionApple")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<InsertTransactionResponseModel> InsertTransactionApple([Header("Authorization")] string auth_token, [Body] string userdata);

        [Post("/User/UpdateVidoePlayed")]
        [Headers("Content-Type: application/json; charset=UTF-8")]
        Task<VideoPlayedResponseModel> UpdateVideoPlayed([Body] int recordId, bool isVideoPlayed);

        //[Get("/api/User/GetUser/{id}/{UserName}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetUserReponseModel> GetUser([Header("Authorization")] string token, int id, string UserName);

        //[Get("/api/Country/GetCountry/{Id}/{title}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetCountryResponseModel> GetCountry([Header("Authorization")] string token, int id, string title);

        //[Get("/api/State/GetState/{ID}/{CountryId}/{Title}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetStateResponseModel> GetState([Header("Authorization")] string token, int ID, int CountryId, string title);

        //[Get("/api/City/GetCity/{ID}/{StateId}/{Title}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetCityResponseModel> GetCity([Header("Authorization")] string token, int ID, int StateId, string Title);

        //[Get("/api/QuestionPaperSharedWithUsers/GetQuestionPaperSharedWithUsers/{ID}/{UserId}/{PaperID}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetSurveyRecievedResponseModel> GetSurveyRecieved([Header("Authorization")] string token, int ID, int UserId, int paperID);

        //[Post("/api/QuestionPaperSharedWithUsers/InsertSharedQuestionPaperWithUsers")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<ShareSurveyWithUsersResponseModel> ShareSurveyWithUsers([Header("Authorization")] string token, [Body] string userdata);

        //[Post("/api/User/UpdateUser")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<UpdateUserResponseModel> UpdateUserDetail([Header("Authorization")] string token, [Body] string userdata);

        //[Get("/api/PaperAnswersFeedback/GetPaperAnswersFeedback/{UserId}/{PaperID}")]
        //[Headers("Content-Type: application/json; charset=UTF-8")]
        //Task<GetUserwiseSurveyFeedbackResponseModel> GetUserwiseSurveyFeedback([Header("Authorization")] string token, int UserId, int PaperID);

    }
}
