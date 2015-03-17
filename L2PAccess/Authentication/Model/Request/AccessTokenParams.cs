using Refit;

namespace L2PAccess.Authentication.Model.Request
{
    public class AccessTokenParams : TokenEndpointParams
    {
        private const string grantType = "device";

        public override string GrantType
        {
            get { return grantType; }
            set {}
        }

        [AliasAs("code")]
        public string DeviceCode { get; set; }
    }
}