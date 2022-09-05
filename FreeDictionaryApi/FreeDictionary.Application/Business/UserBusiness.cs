using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
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
using static FreeDictionary.Application.Model.UserModel;

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
        public async Task<UserMeModel> GetProfile(string userId)
        {
            var user = await _userRepository.GetByIdAsync(new Guid(userId));
            return new UserMeModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
        public async Task<IList<UserWordAdded>> GetFavorities(string userId)
        {
            var favorities = await _favoriteWordRepository.GetItemAsync(x => x.UserId == new Guid(userId));
            var result = new List<UserWordAdded>();
            foreach (var item in favorities)
            {
                result.Add(new UserWordAdded { Added = item.CreatedIn, Word = item.Word });
            }

            return result;
        }

        public async Task<IList<UserWordAdded>> GetHistory(string userId)
        {
            var histories = await _historyWordRepository.GetItemAsync(x => x.UserId == new Guid(userId));
            var result = new List<UserWordAdded>();
            foreach (var item in histories)
            {
                result.Add(new UserWordAdded { Added = item.CreatedIn, Word = item.Word });
            }

            return result;
        }
    }
}
