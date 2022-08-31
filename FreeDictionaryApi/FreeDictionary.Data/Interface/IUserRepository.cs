using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> AddAsync(string name, string email, string password);
    }
}
