using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
using FreeDictionary.Data.Repository;
using FreeDictionary.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IFavoriteWordRepository _favoriteWordRepository;
        private readonly IHistoryWordRepository _historyWordRepository;
        private readonly IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository, IFavoriteWordRepository favoriteWordRepository, IHistoryWordRepository historyWordRepository)
        {
            _favoriteWordRepository = favoriteWordRepository;
            _historyWordRepository = historyWordRepository;
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(new Guid(userId));
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public async Task<PaginationModel<UserWordAddedModel>> GetFavoritiesAsync(string userId, int page = 1, int limit = 10)
        {
            var favorities = await _favoriteWordRepository.GetByUserIdAsync(userId, page, limit);
            var totalDocs = await _favoriteWordRepository.GetTotalByUserIdAsync(userId);
            var totalPages = totalDocs / limit + (totalDocs % limit > 0 ? 1 : 0);

            return new PaginationModel<UserWordAddedModel>
            {
                Results = favorities.Select(x => new UserWordAddedModel { Added = x.CreatedIn, Word = x.Word }).ToList(),
                TotalDocs = totalDocs,
                Page = page,
                TotalPages = totalPages,
                HasNext = page < totalPages,
                HasPrev = page > 1 && page <= totalPages
            };
        }

        public async Task<PaginationModel<UserWordAddedModel>> GetHistoryAsync(string userId, int page = 1, int limit = 10)
        {
            var histories = await _historyWordRepository.GetByUserIdAsync(userId, page, limit);
            var totalDocs = await _historyWordRepository.GetTotalByUserIdAsync(userId);
            var totalPages = totalDocs / limit + (totalDocs % limit > 0 ? 1 : 0);

            return new PaginationModel<UserWordAddedModel>
            {
                Results = histories.Select(x => new UserWordAddedModel { Added = x.CreatedIn, Word = x.Word }).ToList(),
                TotalDocs = totalDocs,
                Page = page,
                TotalPages = totalPages,
                HasNext = page < totalPages,
                HasPrev = page > 1 && page <= totalPages
            };
        }
    }
}
