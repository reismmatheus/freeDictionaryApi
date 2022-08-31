using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
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
        public UserBusiness()
        {

        }
        public async Task<UserModel.UserMeModel> GetProfile()
        {
            throw new NotImplementedException();
        }
        public Task<IList<UserModel.UserWordAdded>> GetFavorities()
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserModel.UserWordAdded>> GetHistory()
        {
            throw new NotImplementedException();
        }
    }
}
