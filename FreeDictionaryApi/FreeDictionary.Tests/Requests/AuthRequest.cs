using FreeDictionary.Application.Model;
using FreeDictionary.Tests.Dependences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static FreeDictionary.Application.Model.AuthModel;

namespace FreeDictionary.Tests.Requests
{
    public class AuthRequest
    {
        private HttpClient _client;
        public AuthRequest(HttpClient client)
        {
            _client = client;
        }

        public async Task<AuthModel> SinginAsync(string email, string password)
        {
            var responseLogin = await _client.PostAsJsonAsync("/auth/singin", new { email, password });
            var resultLogin = await responseLogin.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthModel>(resultLogin);
        }
    }
}
