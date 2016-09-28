
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;

using IdentityServer4.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Models;
using IdentityServer.Data;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Configuration
{
    public class DbProfileService : IProfileService
    {

        private readonly UserManager<User> _userManager;
        private ApplicationDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger _logger;

        public DbProfileService(UserManager<User> userManager, ILoggerFactory loggerFactory,
            ApplicationDbContext context, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<DbProfileService>();

        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //var subjectId = context.Subject.GetSubjectId();

            //var user = _repository.GetUserById(subjectId);

            //var claims = new List<Claim>
            //{
            //    new Claim(JwtClaimTypes.Subject, user.Id),
            //    new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            //    new Claim(JwtClaimTypes.GivenName, user.FirstName),
            //    new Claim(JwtClaimTypes.FamilyName, user.LastName),
            //    new Claim(JwtClaimTypes.Email, user.Email),
            //    new Claim(JwtClaimTypes.EmailVerified, user.EmailVerified.ToString().ToLower(), ClaimValueTypes.Boolean)
            //};

            //context.IssuedClaims = claims;

            //return Task.FromResult(0);

            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                
                var user = await _userManager.FindByIdAsync(sub);
                var cp = await _userManager.GetClaimsAsync(user);
                var claims = new List<Claim>();

                foreach (var claim in cp)
                {
                    claims.Add(claim);
                }
               

                context.IssuedClaims = claims;
            }


        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var locked = true;

            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                var user = await _userManager.FindByIdAsync(sub);
                if (user != null)
                {
                    locked = await _userManager.IsLockedOutAsync(user);
                }
            }

            context.IsActive = !locked;
        }

    }
}