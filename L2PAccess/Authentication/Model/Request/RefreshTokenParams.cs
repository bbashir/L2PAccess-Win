using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class RefreshTokenParams : TokenEndpointParams
    {
        public const string grantType = "refresh_token";

        public override string GrantType
        {
            get { return grantType; }
            set {}
        }

        [AliasAs("refresh_token")]
        public string RefreshToken { get; set; }
    }
}