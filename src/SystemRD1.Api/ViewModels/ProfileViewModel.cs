using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemRD1.Api.ViewModels
{
    public class ProfileViewModel
    {
        public IEnumerable<ClaimsViewModel> ClaimsViewModel { get; set; }
    }
}
