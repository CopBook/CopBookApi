namespace CopBookApi.Models.Services.Firebase
{
    public class FirebaseSignUpResponse : FirebaseAuthBaseResponse
    {
        public string Kind { get; set; }
        public string Email { get; set; }
        public string LocalId { get; set; }
    }
}