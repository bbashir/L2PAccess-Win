using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using L2PAccess.Authentication.Config;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication
{
    /// <summary>
    /// Automatically injects the access token in an HttpRequest's query parameters and also includes the authentication header. This handler uses the oauthmanager singleton.
    /// </summary>
    public class OAuthHttpClientHandler : HttpClientHandler
    {
        private readonly OAuthConfig config;

        public OAuthHttpClientHandler(OAuthConfig config)
        {
            this.config = config;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authModule = await OAuthManager.Init(config);

                Token token = await authModule.GetToken();
                if (token != null && token.access_token != null && token.TokenIsNotExpired())
                {
                    if (config.InjectQueryParam)
                    {
                        var accessTokenQueryParam = "accessToken=" + token.access_token;
                        var paramsWithAccessToken = string.IsNullOrWhiteSpace(request.RequestUri.Query)
                            ? accessTokenQueryParam
                            : request.RequestUri.Query.Replace("?", accessTokenQueryParam + "&");

                        var uriBuilder = new UriBuilder(request.RequestUri)
                        {
                            Query = paramsWithAccessToken
                        };
                        request.RequestUri = uriBuilder.Uri;
                    }

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                    return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                }
            throw new UnauthorizedAccessException("Access token is invalid! It is either expired or not exists!");
        }
    }
}
