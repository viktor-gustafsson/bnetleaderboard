using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BnetLeaderboard.Models;
using BnetLeaderboard.Models.DomainModels;
using BnetLeaderboard.Models.Enums;
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
            var euResult = await GetLeaderBoardData(Region.Eu, 50);
            var usResult = await GetLeaderBoardData(Region.Us, 50);
            var asiaResult = await GetLeaderBoardData(Region.Asia, 50);

            var result = new ComparativeLadderResult
            {
                EuPlayers = euResult.Players,
                UsPlayers = usResult.Players,
                AsiaPlayers = asiaResult.Players
            };

            return result;
        }

        public async Task<RegionLadderResult> GetLeaderBoardData(Region region, int limit = 0)
        {
            _httpClient = new HttpClient();
            
            var token = await _tokenService.GetToken();
            var url = ConstructUrl(region);
            var uri = $"{url}{token.AccessToken}";

            var response = await _httpClient.GetAsync(uri);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync();

            var apiLadderResult = JsonConvert.DeserializeObject<RootObject>(content);

            var ladderResult = new RegionLadderResult
            {
                Region = region.ToString(),
                Players = limit == 0
                    ? apiLadderResult.LadderTeams.Select(Player.Convert).ToList()
                    : apiLadderResult.LadderTeams.Select(Player.Convert).Take(limit).ToList()
            };

            return ladderResult;
        }

        private static string ConstructUrl(Region region)
        {
            var url = $"https://eu.{LadderUrl}/{(int)region}?access_token=";
            return url;
        }
    }
}