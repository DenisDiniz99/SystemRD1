using System.Collections.Generic;

namespace SystemRD1.Api.Extension
{
    public class ResponseResultLogin
    { 
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public ResponseUser ResponseUser { get; set; }
    }

    public class ResponseUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ResponseUserClaims> Claims { get; set; }
    }

    public class ResponseUserClaims
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
