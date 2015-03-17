using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class TokenInfoEndpointParams : EndpointParams
    {
        [AliasAs("access_token")]
        public string AccessToken { get; set; }
    }
}
