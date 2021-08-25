using System.Collections.Generic;
using SystemRD1.WebApp.Extensions;

namespace SystemRD1.WebApp.Models.User
{
    public class ResponseUserViewModel
    {
        public string AccessToken{ get; set; }
        public double ExpiresIn { get; set; }
        public ResponseUserTokenViewModel ResponseUser { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class ResponseUserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ResponseClaimsViewModel> Claims { get; set; }
    }

    public class ResponseClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
