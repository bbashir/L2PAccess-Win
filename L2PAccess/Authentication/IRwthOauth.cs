using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2PAccess.Authentication.Model;
using L2PAccess.Authentication.Model.Request;
using L2PAccess.Authentication.Model.Response;
using Refit;

namespace L2PAccess.Authentication
{
    public interface IRwthOauth
    {
        [Post("/oauth2waitress/oauth2.svc/code")]
        Task<Code> GetCode([Body(BodySerializationMethod.UrlEncoded)] CodeEndpointParams parameters);

        [Post("/oauth2waitress/oauth2.svc/token")]
        Task<Token> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)] AccessTokenParams parameters);

        [Post("/oauth2waitress/oauth2.svc/token")]
        Task<Token> GetRefreshToken([Body(BodySerializationMethod.UrlEncoded)] RefreshTokenParams parameters);

        [Post("/oauth2waitress/oauth2.svc/token")]
        Task<Token> InvalidateAccessToken([Body(BodySerializationMethod.UrlEncoded)] InvalidateTokenParams parameters);

        [Post("/oauth2waitress/oauth2.svc/tokeninfo")]
        Task<Token> GetAccessTokenInfo([Body(BodySerializationMethod.UrlEncoded)] TokenEndpointParams parameters);
    }

}
