namespace CopBookApi.Models.Api.Auth
{
    public class AccountVerificationEmailRequest
    {
        public readonly string RequestType = "VERIFY_EMAIL";

        public string IdToken { get; set; }
    }
}