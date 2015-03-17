using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class EndpointParams
    {
        [AliasAs("client_id")]
        public string ClientId { get; set; }
    }
}