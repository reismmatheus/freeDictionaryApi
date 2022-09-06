using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Repository
{
    public class WordRepository : Repository<Word>, IWordRepository
    {
        private readonly FreeDictionaryContext _context;
        public WordRepository(FreeDictionaryContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Word> words)
        {
            await _context.AddRangeAsync(words);
            await _context.SaveChangesAsync();
        }

        public async Task TruncateTableAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Words]");
        }

        public async Task<IList<Word>> GetBySearchAsync(string search, int page = 1, int limit = 10)
        {
            var query = (from w in _context.Words
                        where w.Name.Contains(search)
                        select w).Skip(page).Take(limit);

            return query.ToList();
        }
    }
}
