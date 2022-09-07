
namespace FreeDictionary.Service.FreeDictionaryApi
{
    public interface IFreeDictionaryApiClient
    {
        Task<object> GetWord(string word);
        Task<List<string>> DownloadWords();
    }
}
