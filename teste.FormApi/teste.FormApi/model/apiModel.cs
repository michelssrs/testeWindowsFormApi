using System;

namespace teste.FormApi
{
    public class tokenObj
    {
        public String token_key { get; set; }
        public String token_type { get; set; }
        public String expires_in { get; set; }
        public String access_token { get; set; }
    }
    public class tokenUserObj
    {
        public String url { get; set; }
        public String user { get; set; }
        public String password { get; set; }
    }
}