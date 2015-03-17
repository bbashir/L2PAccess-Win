using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class TokenEndpointParams : EndpointParams
    {
        [AliasAs("grant_type")]
        public virtual string GrantType { get; set; }
    }
}