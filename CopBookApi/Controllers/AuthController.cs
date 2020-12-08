using CopBookApi.Interfaces.Api.Auth;
using CopBookApi.Interfaces.Services.Auth;
using CopBookApi.Models.Api.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;
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
        public async Task<dynamic> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                return await auth.RefreshToken(request);
            }
            catch (Exception e)
            {
                return HandleAuthException(e);
            }
        }

        [HttpPost]
        public async Task<dynamic> UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                StringValues idTokenFromHeader;
                bool hasAuthHeader = HttpContext.Request.Headers.TryGetValue("Authorization", out idTokenFromHeader);
                if (!hasAuthHeader) // TODO: Refactor this to a shared Authorization scheme once implemented
                {
                    throw new AuthFailedException("Id Token is required for this call", HttpStatusCode.Forbidden);
                }
                request.IdToken = idTokenFromHeader.ToString().Split(' ')[1];
                return await auth.UpdateProfile(request);
            }
            catch (Exception e)
            {
                return HandleAuthException(e);
            }
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

        private IActionResult HandleAuthException(Exception e)
        {
            if (e is AuthFailedException exception)
            {
                switch (exception.ResponseCode)
                {
                    case (int)HttpStatusCode.Forbidden:
                        return new UnauthorizedObjectResult(new { exception.ErrorMessage });
                    case (int)HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(new { exception.ErrorMessage });
                    default:
                        return StatusCode(exception.ResponseCode);
                }
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
