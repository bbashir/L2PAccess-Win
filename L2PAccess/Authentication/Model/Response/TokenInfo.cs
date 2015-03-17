namespace L2PAccess.Authentication.Model.Response
{
    public class TokenInfo
    {
        public string audience { get; set; }
        public TokenInfoStatus state { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int interval { get; set; }
    }

    public enum TokenInfoStatus
    {
        valid, expired
    }
}
