using FreeDictionary.Application.Interface;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using FreeDictionary.Service.FreeDictionaryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Business
{
    public class EntriesBusiness : IEntriesBusiness
    {
        private readonly IFavoriteWordRepository _favoriteWordRepository;
        private readonly IHistoryWordRepository _historyWordRepository;
        private readonly IFreeDictionaryApiClient _freeDictionaryApiClient;
        public EntriesBusiness(IFavoriteWordRepository favoriteWordRepository, IHistoryWordRepository historyWordRepository, IFreeDictionaryApiClient freeDictionaryApiClient)
        {
            _favoriteWordRepository = favoriteWordRepository;
            _historyWordRepository = historyWordRepository;
            _freeDictionaryApiClient = freeDictionaryApiClient;
        }
        public async Task AddFavoriteAsync(string word)
        {
            await _favoriteWordRepository.AddAsync(new FavoriteWord
            {
                Word = word
            });
        }

        public async Task DownloadWordsAsync()
        {
            var words = _freeDictionaryApiClient.DownloadWords();
            throw new NotImplementedException();
        }

        public Task GetAsync(string search, int limit)
        {
            throw new NotImplementedException();
        }

        public Task GetByWordAsync(string word)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFavoriteAsync(string word)
        {
            throw new NotImplementedException();
        }
    }
}
