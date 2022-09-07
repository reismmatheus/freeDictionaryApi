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
        private readonly FreeDictionaryContext _context;
        public FavoriteWordRepository(FreeDictionaryContext context) : base(context)
        {
            _context = context;
        }

        public override async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string userId, string word)
        {
            var entity = _context.FavoriteWords.FirstOrDefault(x => x.Word == word && x.UserId == new Guid(userId));
            _context.FavoriteWords.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<FavoriteWord>> GetByUserIdAsync(string userId, int page, int limit)
        {
            var query = (from f in _context.FavoriteWords
                         where f.UserId == new Guid(userId)
                         select f).Skip(page).Take(limit);

            return query.ToList();
        }

        public async Task<int> GetTotalByUserIdAsync(string userId)
        {
            var query = (from f in _context.FavoriteWords
                         where f.UserId == new Guid(userId)
                         select f);

            return query.Count();
        }
    }
}
