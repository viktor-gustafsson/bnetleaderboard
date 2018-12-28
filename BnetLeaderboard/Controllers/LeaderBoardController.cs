using System.Linq;
using System.Threading.Tasks;
using BnetLeaderboard.Models.Enums;
using BnetLeaderboard.Models.ResourceModels;
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


        public async Task<IActionResult> Top50(Region region)
        {
            var apiLadderResult = await _battleNetService.GetLeaderBoardData(region);

            return View(apiLadderResult);
        }

        public async Task<IActionResult> Top50Comparison()
        {
            var comparativeData = await _battleNetService.GetComparativeData();
            
            return View(comparativeData);
        }
    }
}