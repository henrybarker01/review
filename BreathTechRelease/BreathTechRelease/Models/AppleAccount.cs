﻿using System;
namespace BreathTechRelease.Models
{
        public class AppleAccount
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Token { get; set; }
            public string RealUserStatus { get; set; }
            public string UserId { get; set; }
        public string Password { get; set; }
    }

        public enum AppleSignInCredentialState
        {
            Authorized,
            Revoked,
            NotFound,
            Unknown
        }
}