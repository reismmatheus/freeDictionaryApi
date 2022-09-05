using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeDictionary.Application.Model.UserModel;

namespace FreeDictionary.Application.Interface
{
    public interface IUserBusiness
    {
        Task<UserMeModel> GetProfile(string userId);
        Task<IList<UserWordAdded>> GetHistory(string userId);
        Task<IList<UserWordAdded>> GetFavorities(string userId);
    }
}
