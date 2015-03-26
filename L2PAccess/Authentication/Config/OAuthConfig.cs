using System;
using System.Collections.Generic;

namespace L2PAccess.Authentication.Config
{
    /// <summary>
    /// Configuration items for the OAuth system
    /// </summary>
    public class OAuthConfig
    {
        public bool InjectQueryParam { get; set; }

        public string OauthServerUrl { get; set; }

        public string ClientId { get; set; }

        public IList<string> Scopes { get; set; }

        public string Scope
        {
            get
            {
                return String.Join(" ", Scopes);
            }
        }
    }
}
