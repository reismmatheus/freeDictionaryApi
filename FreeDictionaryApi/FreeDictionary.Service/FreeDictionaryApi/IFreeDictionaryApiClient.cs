using FreeDictionary.Service.FreeDictionaryApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Service.FreeDictionaryApi
{
    public interface IFreeDictionaryApiClient
    {
        Task<object> GetWord(string word);
        Task<List<string>> DownloadWords();
    }
}
