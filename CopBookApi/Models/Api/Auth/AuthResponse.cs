using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Api.Auth
{
    public class AuthResponse : IAuthResponse
    {

        // in the constructor
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }
    }
}