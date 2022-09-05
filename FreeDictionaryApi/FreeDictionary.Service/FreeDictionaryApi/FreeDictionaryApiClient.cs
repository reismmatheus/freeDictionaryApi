using FreeDictionary.Service.FreeDictionaryApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeDictionary.Service.FreeDictionaryApi
{
    public class FreeDictionaryApiClient : IFreeDictionaryApiClient
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl;
        private readonly string _fileUrl;
        private readonly string _urlFile;
        public FreeDictionaryApiClient(HttpClient client)
        {
            _client = client;
            _apiUrl = "https://api.dictionaryapi.dev/api/v2/entries/en";
            _fileUrl = "https://raw.githubusercontent.com/meetDeveloper/freeDictionaryAPI/master/meta/wordList/english.txt";
        }
        public async Task<FreeDictionaryApiResult?> GetWord(string word)
        {
            var response = await _client.GetAsync($"{_apiUrl}/{word}");
            var result = JsonSerializer.Deserialize<List<FreeDictionaryApiResult>>(response.ToString());
            return result.FirstOrDefault();
        }
        public async Task<List<string>> DownloadWords()
        {
            var response = await _client.GetAsync($"{_fileUrl}");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = responseString.Split("\n").ToList();
            return result;
        }
    }
}
