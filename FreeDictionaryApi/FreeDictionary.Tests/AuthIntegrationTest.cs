using FreeDictionary.API.Controllers;
using FreeDictionary.Application.Business;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Context;
using FreeDictionary.Data.Repository;
using FreeDictionary.Tests.Dependences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Tests
{
    public class AuthIntegrationTest
    {
        private HttpClient _client;
        public AuthIntegrationTest()
        {
            _client = new DependencesFixture().Client;
        }

        [Theory]
        [InlineData("test3", "test3@email.com", "test")]
        public async Task Test_Singup(string name, string email, string password)
        {
            var response = await _client.PostAsJsonAsync("/auth/singup", new { name, email, password });

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("email@email.com", "123")]
        public async Task Test_Singin(string email, string password)
        {
            var response = await _client.PostAsJsonAsync("/auth/singin", new { email, password });

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
