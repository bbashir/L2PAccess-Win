using System.Collections.Generic;

namespace L2PAccess.Authentication.Config
{
    public class RwthConfig : OAuthConfig
    {
        public static OAuthConfig Create(string clientId)
        {
            return new OAuthConfig
            {
                ClientId = clientId,
                OauthServerUrl = "https://oauth.campus.rwth-aachen.de/",
                InjectQueryParam = true,
                Scopes = new List<string>
                {
                    "l2p.rwth", 
                    "campus.rwth",
                    "l2p2013.rwth"
                }
            };
        }
    }
}
