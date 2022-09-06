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
        Task<object?> GetByWordAsync(string userId, string word);
        Task<bool> DownloadWordsAsync();
        Task<bool> AddFavoriteAsync(string userId, string word);
        Task RemoveFavoriteAsync(string userId, string word);
    }
}
