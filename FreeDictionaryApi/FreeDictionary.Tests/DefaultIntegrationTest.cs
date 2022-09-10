using FreeDictionary.Tests.Dependences;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace FreeDictionary.Tests
{
    public class DefaultIntegrationTest
    {
        private HttpClient _client;
        public DefaultIntegrationTest()
        {
            _client = new DependencesFixture().Client;
        }

        [Fact]
        public async Task Test_Index()
        {
            var response = await _client.GetAsync("");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
