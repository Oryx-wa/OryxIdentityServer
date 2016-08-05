using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                new Scope
                {
                    Name = "api.todo",
                    DisplayName = "TODO API",
                    Description = "TODO features and data",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                 new Scope
                {
                    Name = "OryxESS.webapi",
                    DisplayName = "Oryx Employee Self Service web api",
                    Description = "Business Logic for Oryx ESS",
                    Type = ScopeType.Resource,

                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("Oryxsecret".Sha256())
                    },
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role"),
                        new ScopeClaim("OryxESS")
                    }

                },
                 new Scope
                {
                    Name = "OryxMCI.webapi",
                    DisplayName = "African Circle MCI Application",
                    Description = "Business Logic for African Circle MCI Application",
                    Type = ScopeType.Resource,

                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("Oryxsecret".Sha256())
                    },
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role"),
                        new ScopeClaim("OryxMCI")
                    }

                },
            };
        }
    }
}
