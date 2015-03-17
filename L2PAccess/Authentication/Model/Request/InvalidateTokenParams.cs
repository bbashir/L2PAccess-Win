using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class InvalidateTokenParams : TokenEndpointParams
    {
        private const string grantType = "invalidate";

        public override string GrantType
        {
            get { return grantType; }
            set {}
        }

        [AliasAs("refresh_token")]
        public string RefreshToken { get; set; }
    }
}