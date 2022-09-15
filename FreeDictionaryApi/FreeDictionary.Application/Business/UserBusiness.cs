using AutoMapper;
using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
using FreeDictionary.Data.Repository;
using FreeDictionary.Domain;
using Microsoft.AspNet.Identity;
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
        private readonly IMapper _mapper;
        private readonly IFavoriteWordRepository _favoriteWordRepository;
        private readonly IHistoryWordRepository _historyWordRepository;
        private readonly IUserRepository _userRepository;
        public UserBusiness(
            IMapper mapper, 
            IUserRepository userRepository, 
            IFavoriteWordRepository favoriteWordRepository, 
            IHistoryWordRepository historyWordRepository)
        {
            _mapper = mapper;
            _favoriteWordRepository = favoriteWordRepository;
            _historyWordRepository = historyWordRepository;
            _userRepository = userRepository;
        }

        public async Task<(UserModel, bool)> GetProfileAsync(string userId) 
        {
            var user = await _userRepository.GetByIdAsync(new Guid(userId));
            var result = _mapper.Map<UserModel>(user);
            return (result, false);
        }

        public async Task<(PaginationModel<UserWordAddedModel>, bool)> GetFavoritesAsync(string userId, int page = 1, int limit = 10)
        {
            var favorities = await _favoriteWordRepository.GetByUserIdAsync(userId, page, limit);

            var result = new PaginationModel<UserWordAddedModel>();
            result.FormatPagination(
                favorities.Select(x => new UserWordAddedModel { Added = x.CreatedIn, Word = x.Word }).ToList(),
                await _favoriteWordRepository.GetTotalByUserIdAsync(userId), 
                page, 
                limit);

            return (result, false);
        }

        public async Task<(PaginationModel<UserWordAddedModel>, bool)> GetHistoryAsync(string userId, int page = 1, int limit = 10)
        {
            var histories = await _historyWordRepository.GetByUserIdAsync(userId, page, limit);

            var result = new PaginationModel<UserWordAddedModel>();
            result.FormatPagination(
                histories.Select(x => new UserWordAddedModel { Added = x.CreatedIn, Word = x.Word }).ToList(),
                await _historyWordRepository.GetTotalByUserIdAsync(userId),
                page,
                limit);

            return (result, false);
        }
    }
}
