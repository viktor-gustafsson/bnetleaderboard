using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BnetLeaderboard.Infrastructure;
using Newtonsoft.Json;

namespace BnetLeaderboard.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private HttpClient _httpClient;
        private readonly BattleNetSettings _battleNetSettings;
        private Token _token;

        public TokenService(BattleNetSettings battleNetSettings)
        {
            _battleNetSettings = battleNetSettings;
            _token = null;
        }

        public async Task<Token> GetToken()
        {
            if (_token == null || TokenExpired())
            {
                _token = await GetAccessToken();
            }

            return _token;
        }

        private bool TokenExpired()
        {
            return DateTime.UtcNow > _token.Expires;
        }

        private async Task<Token> GetAccessToken()
        {
            _httpClient = ConfigureClient();

            var tokenUri = $"https://eu.{_battleNetSettings.TokenUri}";

            var requestBody = CreateRequestBody();

            //Request Token
            var request = await _httpClient.PostAsync(tokenUri, requestBody);

            if (!request.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            var response = await request.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Token>(response);
        }

        private static FormUrlEncodedContent CreateRequestBody()
        {
            //Prepare Request Body
            var requestData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            var requestBody = new FormUrlEncodedContent(requestData);
            return requestBody;
        }

        private HttpClient ConfigureClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{_battleNetSettings.ClientId}:{_battleNetSettings.ClientSecret}")));

            return client;
        }
    }
}