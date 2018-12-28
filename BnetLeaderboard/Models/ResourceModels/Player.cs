using System.Linq;
using BnetLeaderboard.Models.DomainModels;

namespace BnetLeaderboard.Models.ResourceModels
{
    public class Player
    {
        private string _favoriteRace;
        private string _displayName;

        public string FavoriteRace
        {
            get => GetRace();
            set => _favoriteRace = value;
        }

        public string DisplayName
        {
            get => GetName();
            set => _displayName = value;
        }

        public string ClanTag { get; set; }
        public int PreviousRank { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Mmr { get; set; }
        public int JoinTimestamp { get; set; }
        
        
        public string NoClanName => GetName(false);

        private string GetName(bool includeTag = true)
        {
            if(!includeTag)
                return _displayName;
                
            return !string.IsNullOrEmpty(ClanTag) ? $"[{ClanTag}] {_displayName}" : _displayName;
        }

        private string GetRace()
        {
            switch (_favoriteRace)
            {
                case "terran":
                    return "Terran";
                case "zerg":
                    return "Zerg";
                case "protoss":
                    return "Protoss";
            }

            return "Random";
        }

        public static Player Convert(LadderTeam team)
        {
            var player = team.TeamMembers.FirstOrDefault();

            return new Player
            {
                Mmr = team.Mmr,
                DisplayName = player.DisplayName,
                FavoriteRace = player.FavoriteRace,
                ClanTag = player.ClanTag,
                Wins = team.Wins,
                Losses = team.Losses,
                Points = team.Points,
                PreviousRank = team.PreviousRank,
                JoinTimestamp = team.JoinTimestamp
            };
        }
    }
}