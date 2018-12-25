using System;
using Newtonsoft.Json;

namespace BnetLeaderboard.Services.TokenService
{
    public class Token
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("token_type")] public string TokenType { get; set; }
        public DateTime Expires { get; private set; }

        public Token()
        {
            Expires = DateTime.UtcNow.AddHours(23).AddMinutes(55);
        }
    }
}