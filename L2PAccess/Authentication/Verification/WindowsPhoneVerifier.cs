using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Security.Authentication.Web;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Verification
{
    /// <summary>
    /// Handle verification on windows phone
    /// </summary>
    public class WindowsPhoneVerifier

#if WINDOWS_PHONE_APP    
        : UserCodeVerifier
#endif

    {

#if WINDOWS_PHONE_APP

        private Code _code;
        private Func<WebAuthenticationResult, Code, Task<Token>> _fetchAccessTokenAsyncFunc;

        protected override async Task<Token> Verify(Code code, Func<WebAuthenticationResult, Code, Task<Token>> fetchAccessTokenAsyncFunc, Uri startUri)
        {
            _code = code;
            _fetchAccessTokenAsyncFunc = fetchAccessTokenAsyncFunc;
            WebAuthenticationBroker.AuthenticateAndContinue(startUri,WebAuthenticationBroker.GetCurrentApplicationCallbackUri());
            return await fetchAccessTokenAsyncFunc(null, code);
        }

        public async Task<Token> ContinueVerification(WebAuthenticationBrokerContinuationEventArgs args)
        {
            var authenticationResult = args.WebAuthenticationResult;
            return await _fetchAccessTokenAsyncFunc(authenticationResult, _code);
        }
#endif

    }
}