using System;
using Newtonsoft.Json;

namespace CopBookApi.Models.Api.Auth
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "grant_type")]
        public static string GrantType { get { return "refresh_token"; } }
    }
}
