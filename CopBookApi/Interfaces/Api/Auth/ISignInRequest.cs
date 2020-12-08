namespace CopBookApi.Interfaces.Api.Auth
{
    public interface ISignInRequest
    {
        public string Email { get; }
        public string Password { get; }
    }
}