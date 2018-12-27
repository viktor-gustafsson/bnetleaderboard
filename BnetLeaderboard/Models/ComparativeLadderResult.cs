using System.Collections.Generic;
using BnetLeaderboard.Models.ApiResponseModels;

namespace BnetLeaderboard.Models
{
    public class ComparativeLadderResult
    {
        public List<CompData> Result { get; set; }

        public ComparativeLadderResult()
        {
            Result = new List<CompData>();
        }
    }

    public class CompData
    {
        public LadderTeam EuResult { get; set; }
        public LadderTeam UsResult { get; set; }
        public LadderTeam AsiaResult { get; set; }
    }

}