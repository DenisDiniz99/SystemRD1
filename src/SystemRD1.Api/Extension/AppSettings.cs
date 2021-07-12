using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemRD1.Api.Extension
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}
