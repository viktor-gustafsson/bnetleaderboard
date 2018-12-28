using System.Collections.Generic;

namespace BnetLeaderboard.Models.ResourceModels
{
    public class RegionLadderResult
    {
        public string Region { get; set; }
        public List<Player> Players { get; set; }
    }
}