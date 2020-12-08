using System.Threading.Tasks;
using CopBookApi.Models.Api.Auth;

namespace CopBookApi.Interfaces.Services.Auth
{
    public interface IAuthenticationService
    {
        public Task<AuthResponse> SignUp(SignUpRequest request);
    }
}
