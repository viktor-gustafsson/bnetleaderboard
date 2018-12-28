using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BnetLeaderboard.Models;
using BnetLeaderboard.Models.DomainModels;
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
            var euResult = await GetLeaderBoardData("eu", 50);
            var usResult = await GetLeaderBoardData("us", 50);
            var asiaResult = await GetLeaderBoardData("asia", 50);

            var result = new ComparativeLadderResult
            {
                EuPlayers = euResult.Players,
                UsPlayers = usResult.Players,
                AsiaPlayers = asiaResult.Players
            };

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

            var apiLadderResult = JsonConvert.DeserializeObject<RootObject>(content);

            var ladderResult = new RegionLadderResult
            {
                Region = region,
                Players = limit == 0
                    ? apiLadderResult.LadderTeams.Select(Player.Convert).ToList()
                    : apiLadderResult.LadderTeams.Select(Player.Convert).Take(limit).ToList()
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