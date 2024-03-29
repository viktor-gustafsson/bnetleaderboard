using System.Threading.Tasks;
using BnetLeaderboard.Models;
using BnetLeaderboard.Models.Enums;
using BnetLeaderboard.Models.ResourceModels;

namespace BnetLeaderboard.Services.BattleNetService
{
    public interface IBattleNetService
    {
        Task<RegionLadderResult> GetLeaderBoardData(Region region, int limit = 0);
        Task<ComparativeLadderResult> GetComparativeData();
    }
}