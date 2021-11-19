using System;
using System.Collections.Generic;
using System.Text;

namespace BreathTechRelease.RequestModels
{
    public class ResetPasswordRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
