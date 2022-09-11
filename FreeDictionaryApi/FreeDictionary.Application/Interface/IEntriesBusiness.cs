using FreeDictionary.Application.Model;
using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Interface
{
    public interface IEntriesBusiness
    {
        Task<(PaginationModel<string>, bool)> GetAsync(string search, int page, int limit);
        Task<(object?, bool)> GetByWordAsync(string userId, string word);
        Task<(bool, bool)> DownloadWordsAsync();
        Task<(bool, bool)> AddFavoriteAsync(string userId, string word);
        Task RemoveFavoriteAsync(string userId, string word);
    }
}
