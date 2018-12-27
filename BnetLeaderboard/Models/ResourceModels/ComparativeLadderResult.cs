using System.Collections.Generic;

namespace BnetLeaderboard.Models.ResourceModels
{
    public class ComparativeLadderResult
    {
        public List<RegionalData> Result { get; set; }

        public ComparativeLadderResult()
        {
            Result = new List<RegionalData>();
        }
    }
}