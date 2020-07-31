using Mealmate.Core.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {

        private const string TokenValidationUrl = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}";
        private const string UserInfoUrl = "https://oauth2.googleapis.com/tokeninfo?id_token={0}";
        private readonly GoogleAuthSettings _googleAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleAuthService(GoogleAuthSettings googleAuthSettings, IHttpClientFactory httpClientFactory)
        {
            _googleAuthSettings = googleAuthSettings;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<GoogleUserInfoResult> GetUserInfoAsync(string idToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, idToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleUserInfoResult>(responseAsString);
        }

        public async Task<GoogleTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(TokenValidationUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleTokenValidationResult>(responseAsString);
        }
    }
}
