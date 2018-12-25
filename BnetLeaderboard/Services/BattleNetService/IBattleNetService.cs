using System.Threading.Tasks;
using BnetLeaderboard.Models.ApiResponseModels;

namespace BnetLeaderboard.Services.BattleNetService
{
    public interface IBattleNetService
    {
        Task<ApiLadderResult> GetLeaderBoardData(string region);
    }
}