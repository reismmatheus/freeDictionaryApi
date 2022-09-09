using FreeDictionary.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Tests.Dependences
{
    public class DatabaseFixture
    {
        private readonly string _connectionString;
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public DatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        //context.Database.EnsureDeleted();
                        //context.Database.EnsureCreated();
                        //context.AddRange(
                        //    new Blog { Name = "Blog1", Url = "http://blog1.com" },
                        //    new Blog { Name = "Blog2", Url = "http://blog2.com" });
                        //context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public FreeDictionaryContext CreateContext()
        => new FreeDictionaryContext(
            new DbContextOptionsBuilder<FreeDictionaryContext>()
                .UseSqlServer("Data Source=DESKTOP-M9CNHCN\\SQLEXPRESS;Initial Catalog=freeDictionaryApi;Integrated Security=True")
                .Options);
    }
}
