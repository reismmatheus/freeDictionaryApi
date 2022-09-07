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
    public class HistoryWordRepository : Repository<HistoryWord>, IHistoryWordRepository
    {
        private readonly FreeDictionaryContext _context;
        public HistoryWordRepository(FreeDictionaryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<HistoryWord>> GetByUserIdAsync(string userId, int page, int limit)
        {
            var query = (from f in _context.HistoryWords
                         where f.UserId == new Guid(userId)
                         select f).Skip(page).Take(limit);

            return query.ToList();
        }

        public async Task<int> GetTotalByUserIdAsync(string userId)
        {
            var query = (from f in _context.HistoryWords
                         where f.UserId == new Guid(userId)
                         select f);

            return query.Count();
        }
    }
}
