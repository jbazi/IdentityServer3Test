using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace DemoIdentityServer3_ASP.NET_MVC_Client.IdentityServer
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            //var users = new List<InMemoryUser>
            //{
            //    new InMemoryUser{Subject = "123", Username = "Shema", Password="test"},
            //    Claim = new Claim[]
            //    {
            //        new Claim(Constants.ClaimTypes.Name, "Shema Christophe"),
            //        new Claim(Constants.ClaimTypes.GivenName, "Shema"),
            //        new Claim(Constants.ClaimTypes.FamilyName, "Christophe"),
            //        new Claim(Constants.ClaimTypes.Email, "test12@gmail.com"),
            //        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //        new Claim(Constants.ClaimTypes.Role, "Visionary"),
            //        new Claim(Constants.ClaimTypes.Role, "Dreamer"),
            //    }
            //}
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "secret",
                    Subject = "1",
            
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Role, "Geek"),
                        new Claim(Constants.ClaimTypes.Role, "Foo")
                    }
                }
            };
        }
    }
}