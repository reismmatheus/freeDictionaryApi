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
    public class FavoriteWordRepository : Repository<FavoriteWord>, IFavoriteWordRepository
    {
        public FavoriteWordRepository(FreeDictionaryContext context) : base(context)
        {
        }
    }
}
