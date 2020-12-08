using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Api.Auth
{
    public class AuthResponse : IAuthResponse
    {
        public AuthResponse()
        {

        }

        // in the constructor
        public string IdToken => "IdTokenPlaceholder";

        public string RefreshToken => "RefreshTokenPlaceholder";

        public string ExpiresIn => "ExpiresInPlaceholder";
    }
}