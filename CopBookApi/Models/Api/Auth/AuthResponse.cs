using CopBookApi.Interfaces.Api.Auth;
using CopBookApi.Models.Services.Firebase;

namespace CopBookApi.Models.Api.Auth
{
    public class AuthResponse : IAuthResponse
    {

        // in the constructor
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }

        public AuthResponse(FirebaseAuthBaseResponse apiResponse)
        {
            IdToken = apiResponse.IdToken;
            RefreshToken = apiResponse.RefreshToken;
            ExpiresIn = apiResponse.ExpiresIn;
        }
    }
}