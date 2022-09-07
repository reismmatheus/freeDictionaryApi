using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Interface
{
    public interface IHistoryWordRepository : IRepository<HistoryWord>
    {
        Task<IList<HistoryWord>> GetByUserIdAsync(string userId, int page, int limit);
        Task<int> GetTotalByUserIdAsync(string userId);
    }
}
