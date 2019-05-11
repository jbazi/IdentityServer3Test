using DemoIdentityServer3_ASP.NET_MVC_Client.IdentityServer;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using System.Web;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityServer3.Core;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using IdentityServer3.Core.Logging;

namespace DemoIdentityServer3_ASP.NET_MVC_Client
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            //JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
           // LogProvider.SetCurrentLogProvider(new SimpleDiagnosticLoggerProvider(AppDomain.CurrentDomain));
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    RequireSsl = false,
                    SigningCertificate = LoadCertificate(),

                    LoggingOptions = new LoggingOptions
                    {
                        EnableWebApiDiagnostics = true,
                        EnableHttpLogging = true,
                        WebApiDiagnosticsIsVerbose = true,
                        EnableKatanaLogging = true
                    },

                    //Factory = new IdentityServerServiceFactory()
                    //.UseInMemoryUsers(Users.Get())
                    //.UseInMemoryClients(Clients.Get())
                    //.UseInMemoryScopes(Scopes.Get()),
                    Factory = new IdentityServerServiceFactory()
                           .UseInMemoryUsers(Users.Get())
                           .UseInMemoryClients(Clients.Get())
                           .UseInMemoryScopes(Scopes.Get()),
                    EnableWelcomePage = true
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44321/identity",

                ClientId = "mvc",
                Scope = "openid profile roles",
                RedirectUri = "https://localhost:44321/",
                ResponseType = "id_token",

                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        var id = n.AuthenticationTicket.Identity;

                      // we want to keep first name, last name, subject and roles
                      var givenName = id.FindFirst(Constants.ClaimTypes.GivenName);
                        var familyName = id.FindFirst(Constants.ClaimTypes.FamilyName);
                        var sub = id.FindFirst(Constants.ClaimTypes.Subject);
                        var roles = id.FindAll(Constants.ClaimTypes.Role);

                      // create new identity and set name and role claim type
                      var nid = new ClaimsIdentity(
                            id.AuthenticationType,
                            Constants.ClaimTypes.GivenName,
                            Constants.ClaimTypes.Role);

                      //nid.AddClaim(givenName);
                      //nid.AddClaim(familyName);
                      //nid.AddClaim(sub);
                      //nid.AddClaims(roles);

                      // add some other app specific claim
                      nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    }
                }
              
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            //JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            //{
            //    Authority = "https://localhost:44321/identity",
            //    ClientId = "mvc",
            //    Scope = "openid profile roles",
            //    RedirectUri = "https://localhost:44321/",
            //    ResponseType = "id_token",

            //    SignInAsAuthenticationType = "Cookies",
            //    UseTokenLifetime = false,

            //    Notifications = new OpenIdConnectAuthenticationNotifications
            //    {
            //        SecurityTokenValidated = n =>
            //        {
            //            var id = n.AuthenticationTicket.Identity;

            //            // we want to keep first name, last name, subject and roles
            //            var givenName = id.FindFirst(Constants.ClaimTypes.GivenName);
            //            var familyName = id.FindFirst(Constants.ClaimTypes.FamilyName);
            //            var sub = id.FindFirst(Constants.ClaimTypes.Subject);
            //            var roles = id.FindAll(Constants.ClaimTypes.Role);

            //            // create new identity and set name and role claim type
            //            var nid = new ClaimsIdentity(
            //                id.AuthenticationType,
            //                Constants.ClaimTypes.GivenName,
            //                Constants.ClaimTypes.Role);

            //            //nid.AddClaim(givenName);
            //            //nid.AddClaim(familyName);
            //            //nid.AddClaim(sub);
            //            //nid.AddClaims(roles);

            //            // add some other app specific claim
            //            nid.AddClaim(new Claim("app_specific", "some data"));

            //            n.AuthenticationTicket = new AuthenticationTicket(
            //                nid,
            //                n.AuthenticationTicket.Properties);

            //            return Task.FromResult(0);
            //        }
            //    }
            //});

            //app.UseAuthentication();

        }

        X509Certificate2 LoadCertificate()
        {
            //string path = HttpContext.Current.Server.MapPath("~/IdentityServer/idsrv3test.pfx");
            return new X509Certificate2(
                string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}