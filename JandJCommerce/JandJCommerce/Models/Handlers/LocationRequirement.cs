using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Handlers
{
    public class LocationRequirement : IAuthorizationRequirement
    {
        public string Location { get; set; }

        public LocationRequirement(string location)
        {
            Location = location;
        }
    }
}
