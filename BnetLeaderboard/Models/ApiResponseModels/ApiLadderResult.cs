using System.Collections.Generic;

namespace BnetLeaderboard.Models.ApiResponseModels
{
    public class ApiLadderResult
    {
        public string Region { get; set; }
        public List<LadderTeam> LadderTeams { get; set; }
    }
}