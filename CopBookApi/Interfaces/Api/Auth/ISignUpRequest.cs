namespace CopBookApi.Interfaces.Api.Auth
{
    public interface ISignUpRequest : ISignInRequest
    {
        public string Name { get; }
    }
}