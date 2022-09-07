using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Interface
{
    public interface IWordRepository : IRepository<Word>
    {
        Task AddRangeAsync(List<Word> words);
        Task TruncateTableAsync();
        Task<IList<Word>> GetBySearchAsync(string search, int page, int limit);
        Task<int> GetTotalBySearchAsync(string search);
    }
}
