using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BnetLeaderboard.Models;
using BnetLeaderboard.Models.ResourceModels;
using BnetLeaderboard.Services.TokenService;
using Microsoft.AspNetCore.Http.Features;
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

        public async Task<ComparativeLadderResult> GetComparativeData()
        {
            var euResult = await GetLeaderBoardData("eu");
            var usResult = await GetLeaderBoardData("us");
            var asiaResult = await GetLeaderBoardData("asia");

            var result = new ComparativeLadderResult();
            var index = 0;

            while (index < 50)
            {
                var compData = new RegionalData
                {
                    EuResult = euResult.LadderTeams[index],
                    UsResult = usResult.LadderTeams[index],
                    AsiaResult = asiaResult.LadderTeams[index]
                };

                result.Result.Add(compData);
                index++;
            }

            return result;
        }

        public async Task<RegionLadderResult> GetLeaderBoardData(string region, int limit = 0)
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

            var content = await response.Content.ReadAsStringAsync();

            var apiLadderResult = JsonConvert.DeserializeObject<RegionLadderResult>(content);

            var ladderResult = new RegionLadderResult
            {
                Region = region,
                LadderTeams = limit == 0
                    ? apiLadderResult.LadderTeams
                    : apiLadderResult.LadderTeams.Take(limit).ToList()
            };

            return ladderResult;
        }

        private static string ConstructUrl(string region)
        {
            var regionNumber = 0;
            switch (region)
            {
                case "us":
                    regionNumber = 1;
                    break;
                case "eu":
                    regionNumber = 2;
                    break;
                case "asia":
                    regionNumber = 3;
                    break;
            }

            var url = $"https://eu.{LadderUrl}/{regionNumber}?access_token=";
            return url;
        }
    }
}