using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

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
        /// 添加客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
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
                Password = "password"
            };
            users.Add(user);

            user = new TestUser()
            {
                SubjectId = "2",
                Username = "joyjoy",
                Password = "password"
            };
            users.Add(user);

            return users;
        }

    }
}
