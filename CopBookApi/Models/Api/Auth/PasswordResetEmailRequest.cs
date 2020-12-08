namespace CopBookApi.Models.Api.Auth
{
    public class PasswordResetEmailRequest
    {
        public readonly string RequestType = "PASSWORD_RESET";

        public string Email { get; set; }
    }
}