using System;
using System.Configuration;

using System.Web.Http;

using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.DataHandler.Encoder;

using teste.Api.Providers;

namespace teste.Api
{
    public class Startup
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);
        }
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
                                                                        { 
                                                                            AllowInsecureHttp = true,
                                                                            TokenEndpointPath = new PathString("/oauth/token"),
                                                                            AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                                                                            Provider = new customOAuthProvider(),
                                                                            AccessTokenFormat = new customJwtFormat("teste.Api")
                                                                        };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = "teste.Api";
            String audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            Byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            app.UseJwtBearerAuthentication
                (
                    new JwtBearerAuthenticationOptions
                    {
                        AuthenticationMode = AuthenticationMode.Active,
                        AllowedAudiences = new[] { audienceId },
                        IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                                                        {
                                                            new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                                                        }
                    }
                );
        }
    }
}