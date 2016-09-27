using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            IEnumerable<string> gType = new  List<string>
            {
                "Implicit", "Hybrid", "ResourceOwnerPassword"
            };
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:1861/spa/callback.html"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid", "profile",
                        "api.todo"
                    }
                },
                new Client
                {
                    ClientId = "OryxESS.webapi",
                    ClientName = "Oyrx Self Service",
                    //Enabled = true,
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    //},
                   RedirectUris = new List<string>
                    {
                        "http://localhost:3000/",
                        "http://localhost:4200/",
                        "http://10.211.55.2:4200/"


                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:3000/unauthorized.html",
                        "http://localhost:4200/unauthorized.html",
                        "http://10.211.55.2:4200/unauthorized.html",
                    },
                    AllowedScopes = new List<string>
                    {
                         "openid",
                        "email",
                        "profile",
                        "OryxESS.webapi"
                    },
                    AllowedCorsOrigins = new List<string>
                    {

                        "http://localhost:3000",
                        "http://localhost:4200",
                        "http://10.211.55.2:4200"
                    }
                },
                 new Client
                {
                    ClientId = "OryxMCI.webapi",
                    ClientName = "MCI",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    //},

                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "name",
                        StandardScopes.OfflineAccess.Name,
                        "read",
                        "OryxMCI.webapi"
                    },
                     RedirectUris = new List<string>
                    {
                        "http://localhost:3000/",
                        "http://localhost:4200/",
                        "http://10.211.55.2:4200/"

                    },
                     PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:3000/unauthorized.html",
                        "http://localhost:4200/unauthorized.html",
                        "http://10.211.55.2:4200/unauthorized.html",
                    },
                     AllowedCorsOrigins = new List<string>
                    {

                        "http://localhost:3000",
                        "http://localhost:4200",
                        "http://10.211.55.2:4200"
                    }
                },
                new Client
                {
                    ClientId = "OryxMCIP.webapi",
                    ClientName = "MCI",
                    Enabled = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        StandardScopes.OfflineAccess.Name,
                        "read",
                        "OryxMCI.webapi"
                    },
                     RedirectUris = new List<string>
                    {
                        "http://localhost:3000/",
                        "http://localhost:4200/",
                        "http://10.211.55.2:4200/"

                    },
                     PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:3000/unauthorized.html",
                        "http://localhost:4200/unauthorized.html",
                        "http://10.211.55.2:4200/unauthorized.html",
                    },
                     AllowedCorsOrigins = new List<string>
                    {

                        "http://localhost:3000",
                        "http://localhost:4200",
                        "http://10.211.55.2:4200"
                    }
                },
            };
        }
    }
}
