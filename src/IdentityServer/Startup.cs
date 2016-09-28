using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.Configuration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.Validation;
using IdentityServer.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using IdentityModel;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /*
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.AuthenticationOptions.AuthenticationScheme = "Cookies";
            })
            .AddInMemoryClients(Clients.Get())
            .AddInMemoryScopes(Scopes.Get())
            .SetTemporarySigningCredential();

            services.AddTransient<IProfileService, AspIdProfileService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ResouceOwnerProfileService>();

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //
            services.AddIdentity<User, Role>(options => options.User.AllowedUserNameCharacters = null)
                .AddEntityFrameworkStores<ApplicationDbContext, int>()
                .AddUserStore<UserStore<User, Role, ApplicationDbContext, int>>()
                .AddRoleStore<RoleStore<Role, ApplicationDbContext, int>>()
                //.AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders();
           //





            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, IdentityServerUserClaimsPrincipalFactory>();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasPermission", policy => policy.Requirements.Add(new AuthorizationPolicies.HasPermissionRequirement()));
            });

           //

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }
*/
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.AuthenticationOptions.AuthenticationScheme = "Cookies";
            })
              .AddInMemoryClients(Clients.Get())
              .AddInMemoryScopes(Scopes.Get())
              .SetTemporarySigningCredential();

            // services.AddTransient<IProfileService, AspIdProfileService>();
            services.AddTransient<IProfileService, DbProfileService>();
            // services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ResouceOwnerProfileService>();

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<User, Role>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Cookies";
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            })
               .AddEntityFrameworkStores<ApplicationDbContext, int>()
               .AddUserStore<UserStore<User, Role, ApplicationDbContext, int>>()
               .AddRoleStore<RoleStore<Role, ApplicationDbContext, int>>()
               //.AddRoleManager<RoleManager<Role>>()
               .AddDefaultTokenProviders();

            services.AddTransient<IUserClaimsPrincipalFactory<User>, IdentityServerUserClaimsPrincipalFactory>();

            /*

            services.AddIdentity<User, Role>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
                options.Cookies.ApplicationCookie.AuthenticationScheme = "Cookies";
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            })
            .AddEntityFrameworkStores<ApplicationDbContext, int>()
            .AddUserStore<UserStore<User, Role, ApplicationDbContext, int>>()
            .AddRoleStore<RoleStore<Role, ApplicationDbContext, int>>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, IdentityServerUserClaimsPrincipalFactory>();
             */
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasPermission", policy => policy.Requirements.Add(new AuthorizationPolicies.HasPermissionRequirement()));
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseIdentityServer();


            // add the installer
            app.UseInstaller();



            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = "http://localhost:5000",
                Audience = "http://localhost:5000/resources",
                RequireHttpsMetadata = false
            });

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = "http://0.0.0.0:5000",
                Audience = "http://0.0.0.0:5000/resources",
                RequireHttpsMetadata = false
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
