using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Verification
{
    /// <summary>
    /// Handle verification process on windows
    /// </summary>
    public class WindowsVerifier : UserCodeVerifier
    {
        protected override async Task<Token> Verify(Code code, Func<WebAuthenticationResult, Code, Task<Token>> fetchAccessTokenAsyncFunc, Uri startUri)
        {
            WebAuthenticationResult webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.UseTitle,startUri);
            return await fetchAccessTokenAsyncFunc(webAuthenticationResult, code);
        }
    }
}
