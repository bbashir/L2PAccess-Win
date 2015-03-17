using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class CodeEndpointParams : EndpointParams
    {
        [AliasAs("scope")]
        public string Scopes { get; set; }
    }
}