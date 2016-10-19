using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityServer4.Models;

namespace IdentityServer.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IEnumerable<Claim> optionalClaims;

        
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                if (context.UserName == "tayo.adegbola@oryx-wa.com" && context.Password == "Oryx@101")
                {
                    context.Result = new GrantValidationResult(subject: "tayo.adegbola@oryx-wa.com", authenticationMethod: "custom", claims: optionalClaims);
                    return Task.FromResult(context.Result);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
                    return Task.FromResult(context.Result);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
