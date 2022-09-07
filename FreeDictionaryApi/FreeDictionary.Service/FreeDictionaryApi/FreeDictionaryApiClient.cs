using System.Text.Json;

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
        public async Task<object?> GetWord(string word)
        {
            var response = await _client.GetAsync($"{_apiUrl}/{word}");
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(responseString);
            return result;
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
