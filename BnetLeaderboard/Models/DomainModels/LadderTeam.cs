using System.Collections.Generic;
using System.Linq;
using BnetLeaderboard.Models.ResourceModels;

namespace BnetLeaderboard.Models.DomainModels
{
    public class LadderTeam
    {
        public List<TeamMember> TeamMembers { get; set; }
        public int PreviousRank { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Mmr { get; set; }
        public int JoinTimestamp { get; set; }
    }
}