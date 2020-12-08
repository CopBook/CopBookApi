using CopBookApi.Interfaces.Api.Auth;
using CopBookApi.Models.Api.Auth;

namespace CopBookApi.Interfaces.Controllers
{
    public interface IAuthController
    {
        public IAuthResponse SignUp(SignUpRequest request);
        public IAuthResponse SignIn(ISignInRequest request);
        public IAuthResponse RefreshToken();
        public void SendPasswordResetEmail();
    }
}