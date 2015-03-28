using System;

namespace L2PAccess.Authentication.Model.Response
{
    public class Token
    {
        public string status { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }

        public int expires_in
        {
            get
            {
                return -1;
            }
            set
            {
                if (value != -1)
                {
                    accessTokenExpirationDate = new DateTime(Math.Min(DateTime.Now.AddSeconds(value).Ticks, DateTime.Now.AddMinutes(3).Ticks));
                }
            }
        }

        public string refresh_token { get; set; }

        public DateTime accessTokenExpirationDate { get; set; }

        public bool TokenIsExpired()
        {
            return DateTime.Now >= accessTokenExpirationDate;
        }
    }
}
