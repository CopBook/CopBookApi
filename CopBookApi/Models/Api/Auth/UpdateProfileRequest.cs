namespace CopBookApi.Models.Api.Auth
{
    public class UpdateProfileRequest
    {
        public string IdToken { get; set; }

        public string RefreshToken { get; set; }

        public string DisplayName { get; set; }

        public string PhotoUrl { get; set; }

        public static bool ReturnSecureToken { get { return true; } }
    }
}
