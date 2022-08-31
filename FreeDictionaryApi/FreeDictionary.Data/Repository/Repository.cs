using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly FreeDictionaryContext _context;
        public Repository(FreeDictionaryContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<TEntity>> GetItemAsync(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await _context.Set<TEntity>().Where(wherePredicate).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
