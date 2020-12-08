namespace CopBookApi.Models.Services.Firebase
{
    public class FirebaseSignInResponse : FirebaseSignUpResponse
    {
        public string DisplayName { get; set; }
        public bool Registered { get; set; }
    }
}
