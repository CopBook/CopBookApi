using System;
namespace CopBookApi.Models.Services.Firebase
{
    public class FirebaseAuthBaseResponse
    {
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
