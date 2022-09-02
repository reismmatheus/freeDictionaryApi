﻿using FreeDictionary.Application.Interface;
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
        public async Task AddFavoriteAsync(string word)
        {
            await _favoriteWordRepository.AddAsync(new FavoriteWord
            {
                Word = word
            });
        }

        public async Task DownloadWordsAsync()
        {
            var words = await _freeDictionaryApiClient.DownloadWords();
            var wordsList = new List<Word>();
            foreach (var item in words)
            {
                wordsList.Add(new Word { Name = item });
            }
            await _wordRepository.AddRangeAsync(wordsList);
        }

        public async Task GetAsync(string search, int limit)
        {
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
