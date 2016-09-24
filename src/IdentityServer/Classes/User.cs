using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {

            UserPermissions = new HashSet<UserPermission>();
        }

        public UserType UserType { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

    }
}

namespace IdentityServer.Models.Enums
{
    public enum UserType
    {

        [Display(Name = "Administrator")]
        ADMINISTRATOR,

        [Display(Name = "Normal User")]
        NORMAL_USER
    }
}

