using System.Threading.Tasks;
using BnetLeaderboard.Infrastructure;

namespace BnetLeaderboard.Services.TokenService
{
    public interface ITokenService
    {
        Task<Token> GetToken();
    }
}