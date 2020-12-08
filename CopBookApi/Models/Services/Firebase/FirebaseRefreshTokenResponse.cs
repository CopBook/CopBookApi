using CopBookApi.Interfaces.Api.Auth;
using Newtonsoft.Json;

namespace CopBookApi.Models.Services.Firebase
{
    public class FirebaseRefreshTokenResponse : IAuthResponse
    {
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }
    }
}