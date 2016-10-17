using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityModel;
using System.Security.Claims;

namespace IdentityServer.Configuration
{
    public class AspIdProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public AspIdProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                var user = await _userManager.FindByIdAsync(sub);
                var cp = await _claimsFactory.CreateAsync(user);

                var claims = cp.Claims.ToList();
                if (!context.AllClaimsRequested)
                {
                    claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                }
                //claims.Add(new Claim(JwtClaimTypes., $"{ user.LastName}, {user.FirstName}"));
                claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
                

                claims.Add(new System.Security.Claims.Claim(StandardScopes.Email.Name, user.Email));
                claims.Add(new System.Security.Claims.Claim("port", user.Port));
                claims.Add(new System.Security.Claims.Claim("full_name", $"{ user.LastName}, {user.FirstName}"));


                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
