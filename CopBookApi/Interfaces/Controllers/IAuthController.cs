using CopBookApi.Interfaces.Api.Auth;

namespace CopBookApi.Interfaces.Controllers
{
    public interface IAuthController
    {
        public IAuthResponse SignUp(ISignUpRequest signUpRequest);
        public void SignIn(ISignInRequest signInRequest);
        public IAuthResponse RefreshToken();
        public void SendPasswordResetEmail();
    }
}