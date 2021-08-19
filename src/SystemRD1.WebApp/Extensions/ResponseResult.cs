using System.Collections.Generic;

namespace SystemRD1.WebApp.Extensions
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Errors = new ResponseErrorMessage();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessage Errors { get; set; }
    }

    public class ResponseErrorMessage
    {
        public ResponseErrorMessage()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
    }
}
