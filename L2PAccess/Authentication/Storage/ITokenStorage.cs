using System.Threading.Tasks;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Storage
{
    /// <summary>
    /// Access token storage interface
    /// </summary>
    public interface ITokenStorage
    {
        Task Save(string accessToken, string refreshToken, string accessTokenExpiration);
        Task Save(Token token);

        Task<Token> Read();
    }
}
