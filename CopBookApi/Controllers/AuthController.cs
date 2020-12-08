using CopBookApi.Interfaces.Api.Auth;
using CopBookApi.Interfaces.Services.Auth;
using CopBookApi.Models.Api.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CopBookApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService auth;

        public AuthController(IAuthenticationService auth)
        {
            this.auth = auth;
        }

        [HttpPost]
        public IAuthResponse RefreshToken()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void SendPasswordResetEmail()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<dynamic> SignIn(SignInRequest request)
        {
            try
            {
                return await auth.SignIn(request);
            }
            catch (Exception e)
            {
                return HandleAuthException(e);
            }
        }

        [HttpPost]
        public async Task<dynamic> SignUp(SignUpRequest request)
        {
            try
            {
                return await auth.SignUp(request);
            }
            catch (Exception e)
            {
                return HandleAuthException(e);
            }
        }

        private StatusCodeResult HandleAuthException(Exception e)
        {
            if (e is AuthFailedException exception)
            {
                return StatusCode(exception.ResponseCode);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
