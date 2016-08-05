
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System;
using Microsoft.AspNetCore.Identity;
using IdentityServer.Models;


namespace IdentityServer.Configuration
{
    public class ResouceOwnerProfileService : IProfileService
    {
       

        public   Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string subject = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;
            try
            {
                // Get Claims From Database, And Use Subject To Find The Related Claims, As A Subject Is An Unique Identity Of User
                List<string> claimStringList = new List<string> {"role", "OryxMCI" };
                if (claimStringList == null)
                {
                    return Task.FromResult(0);
                }
                else
                {
                    List<Claim> claimList = new List<Claim>();
                    for (int i = 0; i < claimStringList.Count; i++)
                    {
                        claimList.Add(new Claim("role", claimStringList[i]));
                    }
                    context.IssuedClaims = claimList.Where(x => context.RequestedClaimTypes.Contains(x.Type));
                    return Task.FromResult(0);
                }
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public  Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
