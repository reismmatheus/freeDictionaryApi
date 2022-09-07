using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FreeDictionary.Service.FreeDictionaryApi
{
    public class FreeDictionaryApiClient : IFreeDictionaryApiClient
    {
        private readonly HttpClient _client;
        public FreeDictionaryApiClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<object?> GetWordAsync(string apiUrl, string word)
        {
            var response = await _client.GetAsync($"{apiUrl}/{word}");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(responseString);
            return result;
        }
        public async Task<List<string>> DownloadWordsAsync(string fileUrl)
        {
            var response = await _client.GetAsync(fileUrl);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = responseString.Split("\n").ToList();
            return result;
        }
    }
}
