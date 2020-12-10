using CopBookApi.Interfaces.Services.Auth;
using CopBookApi.Interfaces.Services.Logging;
using CopBookApi.Models.Api.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILoggingService logger;

        public AuthController(IAuthenticationService auth, ILoggingService logger)
        {
            this.auth = auth;
            this.logger = logger;
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
                return HandleAuthException(e, "/auth/refreshtoken");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<dynamic> UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                StringValues idTokenFromHeader;
                bool hasAuthHeader = HttpContext.Request.Headers.TryGetValue("Authorization", out idTokenFromHeader);
                request.IdToken = idTokenFromHeader.ToString().Split(' ')[1];
                return await auth.UpdateProfile(request);
            }
            catch (Exception e)
            {
                return HandleAuthException(e, "/auth/updateprofile");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetEmail(PasswordResetEmailRequest request)
        {
            return await auth.SendPasswordResetEmail(request) ? new OkResult() : new StatusCodeResult(500);
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
                return HandleAuthException(e, "/auth/signin");
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
                return HandleAuthException(e, "/auth/signup");
            }
        }

        [HttpGet]
        public IActionResult Test()
        {
            throw new Exception();
        }

        private IActionResult HandleAuthException(Exception e, string endpoint)
        {
            logger.Log(LogLevel.Error, endpoint, e);
            if (e is AuthFailedException exception)
            {
                switch (exception.ResponseCode)
                {
                    case (int)HttpStatusCode.Forbidden:
                        return new UnauthorizedObjectResult(new { exception.ErrorMessage });
                    case (int)HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(new { exception.ErrorMessage });
                    default:
                        return new StatusCodeResult(exception.ResponseCode);
                }
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
