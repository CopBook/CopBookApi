namespace CopBookApi.Interfaces.Api.Auth
{
    public interface IAuthResponse
    {
        public string IdToken { get; }
        public string RefreshToken { get; }
        public string ExpiresIn { get; }
    }
}