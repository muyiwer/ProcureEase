using ProcureEaseAPI.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using ProcureEaseAPI.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace ProcureEaseAPI.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Users/Login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                //  Provider = new SimpleAuthorizationServerProvider()

            };

            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            ConfigureAuth(app);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            createRoles();
        }

        private void createRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Procurement Officer"))
            {
                var role = new IdentityRole();
                role.Name = "Procurement Officer";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("MDA Administrator "))
            {
                var role = new IdentityRole();
                role.Name = "MDA Administrator";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Head of Department"))
            {
                var role = new IdentityRole();
                role.Name = "Head of Department";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Procurement Head"))
            {
                var role = new IdentityRole();
                role.Name = "Procurement Head";
                roleManager.Create(role);
            }
    }

    }
}