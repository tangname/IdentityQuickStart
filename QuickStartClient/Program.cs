using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickStartClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter()
                       .GetResult();

            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            var discovery = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            //通过用户凭据获取令牌
            //var tokenClient = new TokenClient(discovery.TokenEndpoint, "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            var tokenClient = new TokenClient(discovery.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("ainslee", "password", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
