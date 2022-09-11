using FreeDictionary.Application.Configuration;
using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using FreeDictionary.Service.FreeDictionaryApi;
using FreeDictionary.Service.RedisCache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Business
{
    public class EntriesBusiness : IEntriesBusiness
    {
        private readonly IFavoriteWordRepository _favoriteWordRepository;
        private readonly IHistoryWordRepository _historyWordRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IFreeDictionaryApiClient _freeDictionaryApiClient;
        private readonly AppSettingsConfiguration _appSettingsConfiguration;
        private readonly IRedisCacheClient _redisCacheClient;
        public EntriesBusiness(
            IFavoriteWordRepository favoriteWordRepository, 
            IHistoryWordRepository historyWordRepository, 
            IWordRepository wordRepository, 
            IFreeDictionaryApiClient freeDictionaryApiClient,
            IOptions<AppSettingsConfiguration> appSettingsConfiguration,
            IRedisCacheClient redisCacheClient)
        {
            _favoriteWordRepository = favoriteWordRepository;
            _historyWordRepository = historyWordRepository;
            _wordRepository = wordRepository;
            _freeDictionaryApiClient = freeDictionaryApiClient;
            _appSettingsConfiguration = appSettingsConfiguration.Value;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<(bool, bool)> DownloadWordsAsync()
        {
            var words = await _freeDictionaryApiClient.DownloadWordsAsync(_appSettingsConfiguration.FileWordsUrl);
            if (words == null || words.Count == 0)
                return (false, false);

            var wordsList = new List<Word>();
            foreach (var item in words)
            {
                wordsList.Add(new Word { Name = item, CreatedIn = DateTime.UtcNow });
            }

            await _wordRepository.TruncateTableAsync();
            await _wordRepository.AddRangeAsync(wordsList);

            return (true, false);
        }
        public async Task<(PaginationModel<string>, bool)> GetAsync(string search, int page = 1, int limit = 10)
        {
            if (page <= 0) page = 1;
            if (limit <= 0) limit = 10;

            var cache = await _redisCacheClient.GetAsync<PaginationModel<string>>($"{nameof(GetAsync)}::search={search}::page={page}::limit={limit}");

            if (cache != null) return (cache, true);

            var words = await _wordRepository.GetBySearchAsync(search, page, limit);
            var totalDocs = await _wordRepository.GetTotalBySearchAsync(search);
            var totalPages = totalDocs / limit + (totalDocs % limit > 0 ? 1 : 0);
            var result = new PaginationModel<string>
            {
                Results = words.Select(x => x.Name).ToList(),
                TotalDocs = totalDocs,
                Page = page,
                TotalPages = totalPages,
                HasNext = page < totalPages,
                HasPrev = page > 1 && page <= totalPages
            };
            await _redisCacheClient.SetAsync($"{nameof(GetAsync)}::search={search}::page={page}::limit={limit}", result, true);
            return (result, false);
        }
        public async Task<(object?, bool)> GetByWordAsync(string userId, string word)
        {
            await _historyWordRepository.AddAsync(new HistoryWord { Word = word, UserId = new Guid(userId) });
            var cache = await _redisCacheClient.GetAsync<object>($"{nameof(GetByWordAsync)}::{word}");

            if (cache != null) return (cache, true);

            var result = await _freeDictionaryApiClient.GetWordAsync(_appSettingsConfiguration.FreeDictionaryApiUrl, word);
            await _redisCacheClient.SetAsync($"{nameof(GetByWordAsync)}::{word}", result);
            return (result, false);
        }
        public async Task<(bool, bool)> AddFavoriteAsync(string userId, string word)
        {
            var favoriteWord = await _favoriteWordRepository.GetItemAsync(x => x.UserId == new Guid(userId) && x.Word == word);

            if (favoriteWord == null || favoriteWord.Count > 0) return (false, false);

            await _favoriteWordRepository.AddAsync(new FavoriteWord
            {
                Word = word,
                UserId = new Guid(userId)
            });

            return (true, false);
        }
        public async Task RemoveFavoriteAsync(string userId, string word)
        {
            await _favoriteWordRepository.DeleteAsync(userId, word);
        }
    }
}
