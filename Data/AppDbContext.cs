using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Data
{
    public class AppDbContext
    {
        private readonly SQLiteAsyncConnection _Database;

        public AppDbContext(SQLiteAsyncConnection database)
        {
            _Database = database;
            _Database.CreateTableAsync<Models.SQLiteModels.User>().Wait();
            _Database.CreateTableAsync<Models.SQLiteModels.Comments>().Wait();
            _Database.CreateTableAsync<Models.SQLiteModels.Recipe>().Wait();
            _Database.CreateTableAsync<Models.SQLiteModels.Favorites>().Wait();

        }
        public SQLiteAsyncConnection GetConnection()
        {
            return _Database;
        }
    }
}
