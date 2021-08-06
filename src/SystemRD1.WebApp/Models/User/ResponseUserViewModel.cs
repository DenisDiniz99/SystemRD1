namespace SystemRD1.WebApp.Models.User
{
    public class ResponseUserViewModel
    {
        public string AccessToken{ get; set; }
        public double ExpiresIn { get; set; }
        public TokenUserViewModel TokenUser { get; set; }
    }
}
