/*
 * @Author: Atilla Tanrikulu 
 * @Date: 2018-04-16 10:10:45 
 * @Last Modified by: Atilla Tanrikulu
 * @Last Modified time: 2018-06-11 10:46:21
 */
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;


namespace SSO.Web
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource {
                    Name = "role",
                    DisplayName="Your role names",
                    Description="Your role names and role codes",
                    UserClaims = { "role", "admin", "user"},
                    ShowInDiscoveryDocument=true
                }
            };
        }
        // authorized applications, protedted resources
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource(){
                    Name = "core.api",
                    DisplayName ="Core.API",
                    UserClaims = {"name", "role", "email"}
                },
                new ApiResource("alarm.api", "Alarm.API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients() =>
            // client credentials client
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "ConsoleClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets ={new Secret("*****".Sha256())},
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "core.api",
                        "alarm.api"
                        }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "jsclient",
                    ClientName = "JavaScriptClient",
                    AllowedGrantTypes =  GrantTypes.Implicit,
                    ClientSecrets ={new Secret("*****".Sha256())},
                    RedirectUris = { "http://localhost:5003/callback.html","http://localhost:5003" },
                    PostLogoutRedirectUris = {  "http://localhost:5003/index.html" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "core.api",
                        "alarm.api"
                    },
                    AllowedCorsOrigins = new List<string>{
                        "http://127.0.0.1:5003",
                        "http://127.0.0.1:5004",
                        "http://127.0.0.1:5005",
                        "http://127.0.0.1:5006",
                        "http://127.0.0.1:5007",
                        "http://localhost:5003",
                        "http://localhost:5004",
                        "http://localhost:5005",
                        "http://localhost:5006",
                        "http://localhost:5007"
                        },
                    RequireConsent = true,
                    AllowAccessTokensViaBrowser=true
                },
                
                // Angular client core service
                new Client
                {
                    ClientId = "cweb",
                    ClientName = "cweb",
                    AccessTokenLifetime = 60*60,// 60 minutes
                    AllowedGrantTypes =  GrantTypes.Implicit,
                    AlwaysSendClientClaims=true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    ClientSecrets ={new Secret("*****".Sha256())},
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = { "openid", "profile", "email", "role", "core.api" },
                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = new List<string>{
                        "http://127.0.0.1:4200", // web
                        "http://127.0.0.1:5001", // api
                        "http://localhost:4200",
                        "http://localhost:5001",
                    }
                },

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = true,
                    ClientSecrets ={new Secret("*****".Sha256())},
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "core.api",
                        "alarm.api"
                    },
                    AllowOfflineAccess = true
                }
            };

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "systemuser",
                    Password = "123",
                    Claims = {
                        new Claim(JwtClaimTypes.Name,"Systemuser"),
                        new Claim(JwtClaimTypes.Role,"system"),
                        new Claim(JwtClaimTypes.Email, "systemuser@mycompany.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "adminuser",
                    Password = "123",
                    Claims = {
                        new Claim(JwtClaimTypes.Name,"Adminuser"),
                        new Claim(JwtClaimTypes.Role,"admin"),
                        new Claim(JwtClaimTypes.Email, "adminuser@mycompany.com")                    }
                },
                new TestUser
                {
                    SubjectId = "4",
                    Username = "testuser",
                    Password = "123",
                    Claims = {
                        new Claim(JwtClaimTypes.Name,"Testuser"),
                        new Claim(JwtClaimTypes.Role,"test"),
                        new Claim(JwtClaimTypes.Email, "testuser@mycompany.com")
                    }
                }

            };
        }
    }
}