using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Models.Services.Firebase
{
    public class FirebaseAuthBaseResponse : IAuthResponse
    {
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }
    }
}
