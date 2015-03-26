using System.Threading.Tasks;

namespace L2PAccess.Authentication
{
    /// <summary>
    /// This is a singlaton class to access the oauth module, so it make sure only 1 instance are refreshing/accessing the access token in different L2P clients.
    /// </summary>
    public sealed class OAuthManager 
    {
        public OAuth2Module AuthModule { get; set; }
        private static readonly OAuthManager instance = new OAuthManager();

        private OAuthManager()
        {
        }

        public static OAuthManager Instance
        {
            get { return instance; }
        }

        public static async Task<OAuth2Module> Init(Config.OAuthConfig config)
        {
            return Instance.AuthModule ?? (Instance.AuthModule = await OAuth2Module.Create(config));
        }
    }
}
