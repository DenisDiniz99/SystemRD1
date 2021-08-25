using System.Collections.Generic;

namespace SystemRD1.Api.Extension
{
    public class ResponseResult
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

    public class ResponseResultErrors
    {
        public ResponseResultErrors()
        {
            Errors = new ResponseResultMessageErrors();
        }
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseResultMessageErrors Errors { get; set; }
    }

    public class ResponseResultMessageErrors
    {
        public ResponseResultMessageErrors()
        {
            Messages = new List<string>();
        }
        public List<string> Messages { get; set; }
    }
}
