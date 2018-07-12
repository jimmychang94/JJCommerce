using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JandJCommerce.Models.ViewModels
{
    public class IndexUserViewModel
    {
        //public IEnumerable<Claim> MyClaims { get; set; }
        public string UserName { get; set; }
        public bool LoggedIn { get; set; }
    }
}
