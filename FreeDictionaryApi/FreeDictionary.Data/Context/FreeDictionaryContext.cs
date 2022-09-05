using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Context
{
    public class FreeDictionaryContext : DbContext
    {
        public FreeDictionaryContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteWord> FavoriteWords { get; set; }
        public DbSet<HistoryWord> HistoryWords { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}
