using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoIdentityServer3_ASP.NET_MVC_Client.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    //Flow = Flows.Implicit,
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "sampleApi"
                    },
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44321/"
                    },

                    AllowAccessToAllScopes = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client
                {
                    Enabled = true,
                    ClientName = "Shema Client",
                    ClientId = "test",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("test".Sha256())
                    },

                    //Flow = Flows.Implicit,
                    Flow = Flows.AuthorizationCode,
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "sampleApi"
                    },
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44321/"
                    },

                    AllowAccessToAllScopes = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };

            // return new[]
            // {
            //     new Client
            //     {
            //         ClientName = "MVC Client",
            //         ClientId = "mvc",
            //         //Flow = Flows.Implicit,
            //         Flow = Flows.ClientCredentials,

            //         RedirectUris = new List<string>
            //         {
            //             "https://localhost:44321/"
            //         },
            //         PostLogoutRedirectUris = new List<string>
            //         {
            //             "https://localhost:44321/"
            //         },
            //         AllowedScopes = new List<string>
            //         {
            //             "openid",
            //             "profile",
            //             "roles",
            //             "sampleApi"
            //         }
            //     },
            //     new Client
            //     {
            //         ClientName = "MVC Client (service communication)",
            //         ClientId = "mvc_service",
            //         //Flow = Flows.Implicit,
            //         Flow = Flows.ClientCredentials,
            //         AllowAccessToAllScopes = true,
            //         AllowAccessToAllCustomGrantTypes = true,

            //         IdentityTokenLifetime = 300,
            //         AccessTokenLifetime = 300,  //5 minutes
            //RequireConsent = false,

            //         ClientSecrets = new List<Secret>
            //         {
            //             new Secret("secret".Sha256())
            //         },
            //         AllowedScopes = new List<string>
            //         {
            //             "openid",
            //             "profile",
            //             "roles",
            //             "sampleApi"
            //         }
            //     }
            // };
        }
    }
}

//https://identityserver.github.io/Documentation/docsv2/overview/jsGettingStarted.html
//https://www.progress.com/documentation/sitefinity-cms/request-access-token-for-calling-web-services
//https://stackoverflow.com/questions/53820626/cant-obtain-token-due-to-unauthorized-client-error