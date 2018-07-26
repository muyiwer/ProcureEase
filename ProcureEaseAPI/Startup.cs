using ProcureEaseAPI.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using ProcureEaseAPI.App_Start;

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

        }
    }
}
