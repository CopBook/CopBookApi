using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Api.Auth
{
    public class AuthResponse : IAuthResponse
    {
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }

        public AuthResponse(IAuthResponse apiResponse)
        {
            IdToken = apiResponse.IdToken;
            RefreshToken = apiResponse.RefreshToken;
            ExpiresIn = apiResponse.ExpiresIn;
        }
    }
}