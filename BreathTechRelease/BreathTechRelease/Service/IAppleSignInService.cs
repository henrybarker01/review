using System;
using System.Threading.Tasks;
using BreathTechRelease.Models;

namespace BreathTechRelease.Service
{
    public interface IAppleSignInService
    {
        bool IsAvailable { get; }
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        void SignIn();
    }
}
