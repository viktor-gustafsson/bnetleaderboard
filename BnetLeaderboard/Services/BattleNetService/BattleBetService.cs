using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BnetLeaderboard.Models;
using BnetLeaderboard.Models.ApiResponseModels;
using BnetLeaderboard.Services.TokenService;
using Newtonsoft.Json;

namespace BnetLeaderboard.Services.BattleNetService
{
    public class BattleNetService : IBattleNetService
    {
        private HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private const string LadderUrl = "api.blizzard.com/sc2/ladder/grandmaster";

        public BattleNetService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<ApiLadderResult> GetLeaderBoardData(string region)
        {
            var url = ConstructUrl(region);
            var token = await _tokenService.GetToken();
            _httpClient = new HttpClient();

            var uri = $"{url}{token.AccessToken}";
            
            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            var result = await response.Content.ReadAsStringAsync();

            var apiLadderResult = JsonConvert.DeserializeObject<ApiLadderResult>(result);
            apiLadderResult.Region = region;

            return apiLadderResult;
        }

        private static string ConstructUrl(string region)
        {
            var regionNumber = region == "us" ? 1 : 2;
            
            var url = $"https://{region}.{LadderUrl}/{regionNumber}?access_token=";
            return url;
        }
    }
}