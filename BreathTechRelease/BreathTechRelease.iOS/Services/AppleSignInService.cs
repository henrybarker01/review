using System;
using System.Threading.Tasks;
using AuthenticationServices;
using BreathTechRelease.iOS.Services;
using BreathTechRelease.Models;
using BreathTechRelease.Service;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppleSignInService))]
namespace BreathTechRelease.iOS.Services
{
    public class AppleSignInService : NSObject, IAppleSignInService, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        public bool IsAvailable => UIDevice.CurrentDevice.CheckSystemVersion(13, 0);

        public async Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId)
        {
            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var credentialState = await appleIdProvider.GetCredentialStateAsync(userId);
            switch (credentialState)
            {
                case ASAuthorizationAppleIdProviderCredentialState.Authorized:
                    // The Apple ID credential is valid.
                    return AppleSignInCredentialState.Authorized;
                case ASAuthorizationAppleIdProviderCredentialState.Revoked:
                    // The Apple ID credential is revoked.
                    return AppleSignInCredentialState.Revoked;
                case ASAuthorizationAppleIdProviderCredentialState.NotFound:
                    // No credential was found, so show the sign-in UI.
                    return AppleSignInCredentialState.NotFound;
                default:
                    return AppleSignInCredentialState.Unknown;
            }

        }

        public void SignIn()
        {
            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var request = appleIdProvider.CreateRequest();
            request.RequestedScopes = new[] { ASAuthorizationScope.Email, ASAuthorizationScope.FullName };

            var authorizationController = new ASAuthorizationController(new[] { request });
            authorizationController.Delegate = this;
            authorizationController.PresentationContextProvider = this;
            authorizationController.PerformRequests();
        }

        #region IASAuthorizationController Delegate

        [Export("authorizationController:didCompleteWithAuthorization:")]
        public void DidComplete(ASAuthorizationController controller, ASAuthorization authorization)
        {
            if (authorization.GetCredential<ASAuthorizationAppleIdCredential>() is ASAuthorizationAppleIdCredential appleIdCredential)
            {
                MessagingCenter.Send<object, AppleAccount>(this, "AppleAccountDetails",
                    new AppleAccount
                    {
                        UserId = appleIdCredential.User,
                        RealUserStatus = Convert.ToString(appleIdCredential.RealUserStatus),
                        Name = NSPersonNameComponentsFormatter.GetLocalizedString(appleIdCredential.FullName, NSPersonNameComponentsFormatterStyle.Default, NSPersonNameComponentsFormatterOptions.Phonetic),
                        Email = appleIdCredential.Email,
                        Token = new NSString(appleIdCredential.IdentityToken, NSStringEncoding.UTF8).ToString()
                    });
            }
            else if (authorization.GetCredential<ASPasswordCredential>() is ASPasswordCredential passwordCredential)
            {
                MessagingCenter.Send<object, AppleAccount>(this, "AppleAccountDetails", new AppleAccount { UserId = passwordCredential.User, Password = passwordCredential.Password });
            }
        }

        [Export("authorizationController:didCompleteWithError:")]
        public void DidComplete(ASAuthorizationController controller, NSError error)
        {
            MessagingCenter.Send<object, string>(this, "ErrorWhileSignIn", error.ToString());
            Console.WriteLine(error);
        }

        #endregion

        #region IASAuthorizationControllerPresentation Context Providing

        public UIWindow GetPresentationAnchor(ASAuthorizationController controller)
        {
            return UIApplication.SharedApplication.KeyWindow;
        }

     

        #endregion

    }


}
