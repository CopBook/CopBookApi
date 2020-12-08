using System;
using System.Net;

namespace CopBookApi.Models.Api.Auth
{
    public class AuthFailedException : Exception
    {
        public readonly string ErrorMessage;
        public readonly int ResponseCode;

        public AuthFailedException(string message, HttpStatusCode responseCode)
        {
            ErrorMessage = message;
            ResponseCode = (int)responseCode;
        }

        public AuthFailedException(string message, int responseCode)
        {
            ErrorMessage = message;
            ResponseCode = responseCode;
        }
    }
}
