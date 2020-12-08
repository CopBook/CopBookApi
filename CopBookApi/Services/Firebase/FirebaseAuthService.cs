using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CopBookApi.Interfaces.Services.Auth;
using CopBookApi.Models.Api.Auth;
using CopBookApi.Models.Services.Firebase;

namespace CopBookApi.Services.Firebase
{
    public class FirebaseAuthService : IAuthenticationService
    {

        private readonly HttpClient http;
        private readonly FirebaseSettings settings;
        private readonly string apiKeyQueryParam;

        public FirebaseAuthService(IHttpClientFactory httpFactory, FirebaseSettings settings)
        {
            http = httpFactory.CreateClient();
            this.settings = settings;
            apiKeyQueryParam = "?key=" + settings.ApiKey;
        }

        public async Task<AuthResponse> SignIn(SignInRequest request)
        {
            string endpoint = "accounts:signInWithPassword";
            var response = await SendRequest(endpoint, request);

            if (response is object && response.StatusCode == HttpStatusCode.OK)
            {
                string bodyAsString = await response.Content.ReadAsStringAsync();
                var parsedResponse = JSON.Parse<FirebaseSignInResponse>(bodyAsString);
                return new AuthResponse
                {
                    ExpiresIn = parsedResponse.ExpiresIn,
                    IdToken = parsedResponse.IdToken,
                    RefreshToken = parsedResponse.RefreshToken
                };
            }
            else
            {
                throw new AuthFailedException("Sign In Failed. Please double check the request", response.StatusCode);
            }
        }

        public async Task<AuthResponse> SignUp(SignUpRequest request)
        {
            string endpoint = "accounts:signUp";
            var response = await SendRequest(endpoint, request);

            if (response is object && response.StatusCode == HttpStatusCode.OK)
            {
                string bodyAsString = await response.Content.ReadAsStringAsync();
                FirebaseSignUpResponse parsedResponse = JSON.Parse<FirebaseSignUpResponse>(bodyAsString);
                return new AuthResponse
                {
                    ExpiresIn = parsedResponse.ExpiresIn,
                    IdToken = parsedResponse.IdToken,
                    RefreshToken = parsedResponse.RefreshToken
                };
            }
            else
            {
                throw new AuthFailedException("Sign Up Failed. Please double check the request.", response.StatusCode);
            }
        }

        private async Task<HttpResponseMessage> SendRequest(string endpoint, dynamic body)
        {
            try
            {
                string stringifiedRequest = JSON.Stringify(body);
                var httpRequest = new HttpRequestMessage
                {
                    Content = new StringContent(stringifiedRequest, Encoding.UTF8, "application/json"),
                    RequestUri = new Uri(settings.BaseUrl + endpoint + apiKeyQueryParam),
                    Headers =
                    {
                        { "Accept", "*/*" }
                    },
                    Method = HttpMethod.Post
                };
                return await http.SendAsync(httpRequest);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return null;
            }

        }
    }
}
