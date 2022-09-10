using FreeDictionary.Tests.Dependences;
using FreeDictionary.Tests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreeDictionary.Domain;
using System.Net.Http.Json;

namespace FreeDictionary.Tests
{
    public class EntriesIntegrationTest
    {
        private HttpClient _client;
        private AuthRequest _authRequest;
        private string _language = "en";
        public EntriesIntegrationTest()
        {
            _client = new DependencesFixture().Client;
            _authRequest = new AuthRequest(_client);
        }

        [Theory]
        [InlineData("test2@email.com", "test", "project")]
        public async Task Test_Get(string email, string password, string search)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.GetAsync($"/entries/{_language}?search={search}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("test2@email.com", "test", "project")]
        public async Task Test_GetWord(string email, string password, string word)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.GetAsync($"/entries/{_language}/{word}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("test2@email.com", "test", "project")]
        public async Task Test_AddFavoriteWord(string email, string password, string word)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.PostAsync($"/entries/{_language}/{word}/favorite", null);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("test2@email.com", "test", "project")]
        public async Task Test_RemoveFavoriteWord(string email, string password, string word)
        {
            var authResult = await _authRequest.SinginAsync(email, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var response = await _client.DeleteAsync($"/entries/{_language}/{word}/unfavorite");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
