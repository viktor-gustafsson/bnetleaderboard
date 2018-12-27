namespace BnetLeaderboard.Models.ApiResponseModels
{
    public class TeamMember
    {
        private string _favoriteRace;
        private string _displayName;
        public string Id { get; set; }
        public int Realm { get; set; }
        public int Region { get; set; }

        public string NoClanName => GetName(false);

        public string DisplayName
        {
            get => GetName();
            set => _displayName = value;
        }


        public string FavoriteRace
        {
            get => GetRace();
            set => _favoriteRace = value;
        }

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

        public string ClanTag { get; set; }
    }
}