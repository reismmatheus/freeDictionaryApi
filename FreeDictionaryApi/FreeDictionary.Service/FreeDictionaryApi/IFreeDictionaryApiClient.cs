
namespace FreeDictionary.Service.FreeDictionaryApi
{
    public interface IFreeDictionaryApiClient
    {
        Task<object> GetWordAsync(string apiUrl, string word);
        Task<List<string>> DownloadWordsAsync(string fileUrl);
    }
}
