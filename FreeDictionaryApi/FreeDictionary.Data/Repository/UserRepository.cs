using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FreeDictionaryContext context) : base(context)
        {
        }
    }
}
