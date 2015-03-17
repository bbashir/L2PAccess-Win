using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Verification
{
    public class WindowsVerifier : UserCodeVerifier
    {
        protected override async Task<Token> Verify(Code code, Func<WebAuthenticationResult, Code, Task<Token>> fetchAccessTokenAsyncFunc, Uri startUri)
        {
            WebAuthenticationResult webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.UseTitle,startUri);
            return await fetchAccessTokenAsyncFunc(webAuthenticationResult, code);
        }
    }
}
