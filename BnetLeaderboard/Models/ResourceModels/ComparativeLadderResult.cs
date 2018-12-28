using System.Collections.Generic;

namespace BnetLeaderboard.Models.ResourceModels
{
    public class ComparativeLadderResult
    {
        public List<Player> EuPlayers { get; set; }
        public List<Player> UsPlayers { get; set; }
        public List<Player> AsiaPlayers { get; set; }
        
    }
}