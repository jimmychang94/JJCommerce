using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JandJCommerce.Models.Handlers
{
    /// <summary>
    /// This class creates the functionality behind the location policy.
    /// </summary>
    public class LocationHandler : AuthorizationHandler<LocationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LocationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.StreetAddress))
            {
                return Task.CompletedTask;
            }

            var userLocation = context.User.FindFirst(c => c.Type == ClaimTypes.StreetAddress).Value;

            if (userLocation == requirement.Location)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
