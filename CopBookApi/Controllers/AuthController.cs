using CopBookApi.Interfaces.Api.Auth;
using CopBookApi.Interfaces.Controllers;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CopBookApi.Controllers
{   
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public IAuthResponse RefreshToken()
        {
            return null;
        }

        public void SendPasswordResetEmail()
        {
            throw new NotImplementedException();
        }

        [HttpPost("sign-in")]
        async public Task<IActionResult> SignIn()
        {

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync("nater20k@gmail.com");

            var user = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecord.Uid);
            // See the UserRecord reference doc for the contents of userRecord.
            Console.WriteLine($"Successfully fetched user data: {userRecord.Uid}");
            return Ok(user);
        }

        [HttpPost]
        public IAuthResponse SignUp(ISignUpRequest signUp)
        {
            throw new NotImplementedException();
        }
    }
}
