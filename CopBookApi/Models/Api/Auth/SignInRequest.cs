using System;
using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Api.Auth
{
    public class SignInRequest : ISignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool ReturnSecureToken { get { return true; } }
    }
}
