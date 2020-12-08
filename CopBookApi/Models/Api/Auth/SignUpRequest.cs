using System;
using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Api.Auth
{
    public class SignUpRequest : ISignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ReturnSecureToken { get { return true; } }
    }
}
