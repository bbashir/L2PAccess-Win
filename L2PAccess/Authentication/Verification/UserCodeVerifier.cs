using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Verification
{
    public abstract class UserCodeVerifier
    {
        public async Task<Token> VerifyTask(Code code, Func<WebAuthenticationResult, Code, Task<Token>> fetchAccessTokenAsyncFunc)
        {
            var startUri = new Uri(new Uri(code.verification_url), "?q=verify&d=" + code.user_code);
            Debug.WriteLine("Navigating to: " + startUri);
            return await Verify(code, fetchAccessTokenAsyncFunc, startUri);
        }

        protected abstract Task<Token> Verify(Code code,Func<WebAuthenticationResult, Code, Task<Token>> fetchAccessTokenAsyncFunc, Uri startUri);
    }
}