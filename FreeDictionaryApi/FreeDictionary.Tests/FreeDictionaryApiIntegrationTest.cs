using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Tests
{
    public class FreeDictionaryApiIntegrationTest
    {
        [Theory]
        [InlineData("test1", "test1@email.com", "test")]
        public async Task Test_Singup(string name, string email, string password)
        {
            //using (var client = new ClientProvider().client)
            //{
            //    var json = JsonConvert.SerializeObject(new { name, email, password });

            //    var content = new StringContent(json, Encoding.UTF8, "application/json");

            //    var response = await client.PostAsync($"/auth/singup", content);

            //    response.EnsureSuccessStatusCode();

            //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //}
        }
    }
}
