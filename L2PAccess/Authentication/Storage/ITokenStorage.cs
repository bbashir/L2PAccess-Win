using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2PAccess.Authentication.Model;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Storage
{
    public interface ITokenStorage
    {
        Task Save(string accessToken, string refreshToken, string accessTokenExpiration);
        Task Save(Token token);

        Task<Token> Read();
    }
}
