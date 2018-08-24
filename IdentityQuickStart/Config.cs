using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityQuickStart
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
               new ApiResource("api1", "测试API资源")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var list = new List<Client>();

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

            return list;
        }
    }
}
