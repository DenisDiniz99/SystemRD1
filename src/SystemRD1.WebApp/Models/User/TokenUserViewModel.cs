using System.Collections.Generic;

namespace SystemRD1.WebApp.Models.User
{
    public class TokenUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimsUserViewModel> Claims { get; set; }
    }
}
