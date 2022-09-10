using FreeDictionary.Tests.Dependences;
using FreeDictionary.Tests.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static FreeDictionary.Application.Model.AuthModel;

namespace FreeDictionary.Tests
{
    public class UserIntegrationTest
    {
        private HttpClient _client;
        private AuthRequest _authRequest;
        public UserIntegrationTest()
        {
            _client = new DependencesFixture().Client;
            _authRequest = new AuthRequest(_client);
        }

        [Theory]
        [InlineData("test2@email.com", "test")]
        public async Task Test_GetProfile(string email, string password)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.GetAsync("/user/me");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("test2@email.com", "test")]
        public async Task Test_GetHistory(string email, string password)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.GetAsync("/user/me/history");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("test2@email.com", "test")]
        public async Task Test_GetFavorites(string email, string password)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.GetAsync("/user/me/favorites");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
