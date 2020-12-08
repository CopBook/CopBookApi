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
            return await ProcessApiResponse(response);
        }

        public async Task<AuthResponse> SignUp(SignUpRequest request)
        {
            string endpoint = "accounts:signUp";
            var httpResponse = await SendRequest(endpoint, request);
            var parsedRes = await ProcessApiResponse(httpResponse);
            var finalResponse = await UpdateProfile(new UpdateProfileRequest
            {
                DisplayName = request.Name,
                IdToken = parsedRes.IdToken,
                RefreshToken = parsedRes.RefreshToken
            });
            await SendAccountVerificationEmail(new AccountVerificationEmailRequest
            {
                IdToken = finalResponse.IdToken
            });
            return finalResponse;
        }

        public async Task<AuthResponse> UpdateProfile(UpdateProfileRequest request)
        {
            string endpoint = "accounts:update";
            var httpResponse = await SendRequest(endpoint, request);
            _ = await ProcessApiResponse(httpResponse);
            return await RefreshToken(new RefreshTokenRequest
            {
                RefreshToken = request.RefreshToken
            });
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
        {
            string endpoint = "token";
            var httpResponse = await SendRequest(endpoint, request);
            var parsedRes = await ProcessApiResponse(httpResponse, true);
            return parsedRes;
        }

        public async Task<bool> SendAccountVerificationEmail(AccountVerificationEmailRequest request)
        {
            string endpoint = "accounts:sendOobCode";
            var httpResponse = await SendRequest(endpoint, request);
            try
            {
                await ProcessApiResponse(httpResponse);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmail(PasswordResetEmailRequest request)
        {
            string endpoint = "accounts:sendOobCode";
            var httpResponse = await SendRequest(endpoint, request);
            try
            {
                await ProcessApiResponse(httpResponse);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
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

        private async Task<AuthResponse> ProcessApiResponse(HttpResponseMessage response, bool isRefreshTokenResponse = false)
        {
            if (response is object && response.StatusCode == HttpStatusCode.OK)
            {
                string bodyAsString = await response.Content.ReadAsStringAsync();

                if (isRefreshTokenResponse)
                {
                    var parsedResponse = JSON.Parse<FirebaseRefreshTokenResponse>(bodyAsString);
                    return new AuthResponse(parsedResponse);
                }
                else
                {
                    var parsedResponse = JSON.Parse<FirebaseAuthBaseResponse>(bodyAsString);
                    return new AuthResponse(parsedResponse);
                }
            }
            else
            {
                throw new AuthFailedException("Sign Up Failed. Please double check the request.", response.StatusCode);
            }
        }
    }
}
