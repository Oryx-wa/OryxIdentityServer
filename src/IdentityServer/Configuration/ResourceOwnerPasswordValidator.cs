using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //Task<CustomGrantValidationResult> IResourceOwnerPasswordValidator.ValidateAsync(string userName, string password, ValidatedTokenRequest request)
        //{
        //    // Check The UserName And Password In Database, Return The Subject If Correct, Return Null Otherwise
        //    string subject = null;

        //    if (userName == "tayo.adegbola@oryx-wa.com" && password == "Oryx@101")
        //    {
        //        subject = "tayo.adegbola@oryx-wa.com";
        //    }

        //    if (subject == null)
        //    {
        //        var result = new CustomGrantValidationResult("Username Or Password Incorrect");
        //        return Task.FromResult(result);
        //    }
        //    else
        //    {
        //        var result = new CustomGrantValidationResult(subject, "password");
        //        return Task.FromResult(result);
        //    }
        //}
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
