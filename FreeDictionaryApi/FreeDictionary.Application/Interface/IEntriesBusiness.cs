using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Interface
{
    public interface IEntriesBusiness
    {
        Task GetAsync(string search, int limit);
        Task GetByWordAsync(string word);
        Task<bool> DownloadWordsAsync();
        Task AddFavoriteAsync(string word);
        Task RemoveFavoriteAsync(string word);
    }
}
