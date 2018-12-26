using System.Linq;
using System.Threading.Tasks;
using BnetLeaderboard.Models.ApiResponseModels;
using BnetLeaderboard.Services.BattleNetService;
using Microsoft.AspNetCore.Mvc;

namespace BnetLeaderboard.Controllers
{
    public class LeaderBoardController : Controller
    {
        private readonly IBattleNetService _battleNetService;

        public LeaderBoardController(IBattleNetService battleNetService)
        {
            _battleNetService = battleNetService;
        }


        public async Task<IActionResult> Top50(string region)
        {
            var apiLadderResult = await _battleNetService.GetLeaderBoardData(region);

            var ladderResult = new ApiLadderResult
            {
                Region = region,
                LadderTeams = apiLadderResult.LadderTeams.Take(50).ToList()
            };

            return View(ladderResult);
        }
    }
}