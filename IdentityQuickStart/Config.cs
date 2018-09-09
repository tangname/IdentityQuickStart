using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityQuickStart
{
    public class Config
    {
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
               new ApiResource("api1", "测试API资源")
            };
        }

        /// <summary>
        /// 添加OAuth客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            //return GetOAuthClients();

            //return GetOIDCClients();

            return GetHybridClients();
        }

        /// <summary>
        /// 添加OAuth客户端
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Client> GetOAuthClients()
        {
            var list = new List<Client>();

            //使用用户凭据认证
            var client = new Client
            {
                ClientId = "client",
                //非交互用户，使用Clientid/Secret认证
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                //认证的Secret
                ClientSecrets ={
                    new Secret("secret".Sha256())
                },
                //客户端要访问的作用域(Api)
                AllowedScopes = { "api1" }
            };
            list.Add(client);

            //使用资源拥有者密码认证
            client = new Client()
            {
                ClientId = "ro.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets ={
                    new Secret("secret".Sha256())
                },
                //客户端要访问的作用域(Api)
                AllowedScopes = { "api1" }
            };
            list.Add(client);

            return list;
        }

        /// <summary>
        /// 添加OpenID Connect客户端
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Client> GetOIDCClients()
        {
            var list = new List<Client>();

            //使用用户凭据认证
            var client = new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                //交互用户，使用OpenID Connect认证
                AllowedGrantTypes = GrantTypes.Implicit,
                //登录后重定向地址
                RedirectUris = { "http://localhost:5002/signin-oidc" },
                //注销后重定向地址
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                //客户端要访问的作用域(Identity)
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            };

            list.Add(client);

            return list;
        }

        /// <summary>
        /// 添加Hybrid flow客户端
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Client> GetHybridClients()
        {
            var list = new List<Client>();

            //使用用户凭据认证
            var client = new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                //使用Hybrid flow认证
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //客户端密钥
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                //登录后重定向地址
                RedirectUris = { "http://localhost:5002/signin-oidc" },
                //注销后重定向地址
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                //客户端要访问的作用域(Identity)
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1" //保护api
                },
                //允许刷新acess token
                AllowOfflineAccess = true
            };

            list.Add(client);

            return list;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            var users = new List<TestUser>();

            var user = new TestUser()
            {
                SubjectId = "1",
                Username = "ainslee",
                Password = "password",
                Claims = new[]
                {
                    new Claim("name","Ainslee"),
                    new Claim("website","http://ainslee.io")
                }
            };
            users.Add(user);

            user = new TestUser()
            {
                SubjectId = "2",
                Username = "joyjoy",
                Password = "password",
                Claims = new[]
                {
                    new Claim("name","joyjoy"),
                    new Claim("website","http://joyjoy.io")
                }
            };
            users.Add(user);

            return users;
        }

        /// <summary>
        /// 添加身份资源(OpenId Connect)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
