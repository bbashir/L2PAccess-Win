using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using L2PAccess.Authentication.Config;
using L2PAccess.Authentication.Model.Request;
using L2PAccess.Authentication.Model.Response;
using L2PAccess.Authentication.Storage;
using L2PAccess.Authentication.Verification;
using Refit;

namespace L2PAccess.Authentication
{
    /// <summary>
    /// Responsible for managing OAuth access tokens including fetching new ones/storing/retreiving/refreshing them.
    /// </summary>
    public class OAuth2Module
    {
        protected ITokenStorage TokenStorage = new SecureTokenStorage();
        protected Token Token;
        protected IRwthOauth OauthClient;
        protected OAuthConfig Config;
        private UserCodeVerifier verifier;

        public OAuth2Module(OAuthConfig config)
        {
            Config = config;
        }

        public UserCodeVerifier Verifier
        {
            get { return verifier; }
        }

        public async Task<Token> GetToken()
        {
            return await GetTokenAsync();
        }

        public async static Task<OAuth2Module> Create(OAuthConfig config)
        {
            OAuth2Module module = new OAuth2Module(config);
            await module.Init();
            return module;
        }

        public async static Task<OAuth2Module> Create(ITokenStorage storage, OAuthConfig config)
        {
            OAuth2Module module = new OAuth2Module(config)
            {
                TokenStorage = storage
            };
            await module.Init();
            return module;
        }

        public async Task Init()
        {
            OauthClient = RestService.For<IRwthOauth>(new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(Config.OauthServerUrl) });
#if WINDOWS_PHONE_APP
            verifier = new WindowsPhoneVerifier();
#else
            verifier = new WindowsVerifier();
#endif

            try
            {
                Token = await TokenStorage.Read();
            }
            catch (IOException)
            {
                Token = new Token();
            }
        }

        public async Task<Token> GetTokenAsync()
        {
            if (Token.access_token == null || !Token.TokenIsNotExpired())
            {
                if (Token.refresh_token != null)
                {
                    return await RefreshAccessToken();
                }
                else
                {
                    return await GetNewToken();
                }
            }
            return Token;
        }

        protected virtual async Task<Token> RefreshAccessToken()
        {
            var parameters = new RefreshTokenParams()
            {
                ClientId = Config.ClientId,
                RefreshToken = Token.refresh_token
            };
            var refreshed = await OauthClient.GetRefreshToken(parameters);
            refreshed.refresh_token = Token.refresh_token;
            refreshed.expires_in = 3600;
            Token = refreshed;
            await TokenStorage.Save(Token);
            return Token;
        }

        public virtual async Task<Token> GetNewToken()
        {
            var parameters = new CodeEndpointParams()
            {
                ClientId = Config.ClientId,
                Scopes = Config.Scope
            };
            var code = await OauthClient.GetCode(parameters);
            return await Verifier.VerifyTask(code, HandleVerificationResult);
        }

        private async Task<Token> HandleVerificationResult(WebAuthenticationResult authenticationResult, Code code)
        {
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
            CancellationToken token = cancellationTokenSource.Token;
            await PollAccessToken(code.device_code,token);
            return Token;
        }

        private async Task<Token> PollAccessToken(string deviceCode, CancellationToken token)
        {
            const int pollingInterval = 5000;
            while (true)
            {
                //Poll request token
                var parameters = new AccessTokenParams()
                {
                    ClientId = Config.ClientId,
                    DeviceCode = deviceCode
                };

                Token = await OauthClient.GetAccessToken(parameters);
                if (Token.status.Equals("ok"))
                {
                    await TokenStorage.Save(Token);
                    return Token;
                }
                await Task.Delay(pollingInterval, token);
            }
        }
    }
}
