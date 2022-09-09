using FreeDictionary.API.Controllers;
using FreeDictionary.Application.Business;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Context;
using FreeDictionary.Data.Repository;
using FreeDictionary.Tests.Dependences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Tests
{
    public class AuthControllerIntegrationTest
    {
        private AuthController _authController;
        private FreeDictionaryContext _context;
        public AuthControllerIntegrationTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FreeDictionaryContext>();

            builder.UseSqlServer("Data Source=DESKTOP-M9CNHCN\\SQLEXPRESS;Initial Catalog=freeDictionaryApi;Integrated Security=True")
                    .UseInternalServiceProvider(serviceProvider);

            _context = new FreeDictionaryContext(builder.Options);
            _context.Database.Migrate();

            var userRepository = new UserRepository(_context);
            var authBusiness = new AuthBusiness(userRepository, null);
            _authController = new AuthController(authBusiness);
        }


        [Theory]
        [InlineData("test1", "test1@email.com", "test")]
        public async Task Test_Singup(string name, string email, string password)
        {
            var singup = await _authController.Singup(new AuthModel.SingupModel { Email = email, Name = name, Password = password });
            //using (var client = new ClientProvider().client)
            //{
            //    var json = JsonConvert.SerializeObject(new { name, email, password });

            //    var content = new StringContent(json, Encoding.UTF8, "application/json");

            //    var response = await client.PostAsync($"/auth/singup", content);

            //    response.EnsureSuccessStatusCode();

            //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //}
        }
        [Theory]
        [InlineData("email@email.com", "123")]
        public async Task Test_Singin(string email, string password)
        {
            var singup = await _authController.Singin(new AuthModel.SinginModel { Email = email, Password = password });
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
