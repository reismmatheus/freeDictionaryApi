using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
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
        private readonly IWordRepository _wordRepository;
        private readonly IFreeDictionaryApiClient _freeDictionaryApiClient;
        public EntriesBusiness(IFavoriteWordRepository favoriteWordRepository, IHistoryWordRepository historyWordRepository, IWordRepository wordRepository, IFreeDictionaryApiClient freeDictionaryApiClient)
        {
            _favoriteWordRepository = favoriteWordRepository;
            _historyWordRepository = historyWordRepository;
            _wordRepository = wordRepository;
            _freeDictionaryApiClient = freeDictionaryApiClient;
        }

        public async Task<bool> DownloadWordsAsync()
        {
            var words = await _freeDictionaryApiClient.DownloadWords();
            if (words == null || words.Count == 0)
                return false;

            var wordsList = new List<Word>();
            foreach (var item in words)
            {
                wordsList.Add(new Word { Name = item, CreatedIn = DateTime.UtcNow });
            }

            await _wordRepository.TruncateTableAsync();
            await _wordRepository.AddRangeAsync(wordsList);

            return true;
        }
        public async Task<EntriesWordModel> GetAsync(string search, int page = 1, int limit = 10)
        {
            var words = await _wordRepository.GetBySearchAsync(search, page, limit);
            var totalDocs = await _wordRepository.GetTotalBySearchAsync(search);
            var totalPages = totalDocs / limit + (totalDocs % limit > 0 ? 1 : 0);
            return new EntriesWordModel
            {
                Results = words.Select(x => x.Name).ToList(),
                TotalDocs = totalDocs,
                Page = page,
                TotalPages = totalPages,
                HasNext = page < totalPages,
                HasPrev = page > 1 && page <= totalPages
            }; ;
        }
        public async Task<object?> GetByWordAsync(string userId, string word)
        {
            await _historyWordRepository.AddAsync(new HistoryWord { Word = word, UserId = new Guid(userId) });
            var wordResult = await _freeDictionaryApiClient.GetWord(word);
            return wordResult;
        }
        public async Task<bool> AddFavoriteAsync(string userId, string word)
        {
            var favoriteWord = await _favoriteWordRepository.GetItemAsync(x => x.UserId == new Guid(userId) && x.Word == word);

            if (favoriteWord == null || favoriteWord.Count > 0)
                return false;

            await _favoriteWordRepository.AddAsync(new FavoriteWord
            {
                Word = word,
                UserId = new Guid(userId)
            });
            return true;
        }
        public async Task RemoveFavoriteAsync(string userId, string word)
        {
            await _favoriteWordRepository.DeleteAsync(userId, word);
        }
    }
}
