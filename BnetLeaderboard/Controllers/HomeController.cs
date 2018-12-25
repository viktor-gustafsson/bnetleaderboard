using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BnetLeaderboard.Models;
using BnetLeaderboard.Services;
using BnetLeaderboard.Services.BattleNetService;

namespace BnetLeaderboard.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBattleNetService _battleNetService;

        public HomeController(IBattleNetService battleNetService)
        {
            _battleNetService = battleNetService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> LeaderBoard(string region)
        {
            var apiLadderResult = await _battleNetService.GetLeaderBoardData(region);

            return View(apiLadderResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
