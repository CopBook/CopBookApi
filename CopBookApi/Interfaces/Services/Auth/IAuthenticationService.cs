using System.Threading.Tasks;
using CopBookApi.Models.Api.Auth;

namespace CopBookApi.Interfaces.Services.Auth
{
    public interface IAuthenticationService
    {
        public Task<AuthResponse> SignUp(SignUpRequest request);
        public Task<AuthResponse> SignIn(SignInRequest request);
        public Task<AuthResponse> UpdateProfile(UpdateProfileRequest request);
        public Task<AuthResponse> RefreshToken(RefreshTokenRequest request);
        public Task<bool> SendAccountVerificationEmail(AccountVerificationEmailRequest request);
        public Task<bool> SendPasswordResetEmail(PasswordResetEmailRequest request);
    }
}
