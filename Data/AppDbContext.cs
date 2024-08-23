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

        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                // Create the Users table if it doesn't exist
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
            }

        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _Database;
        }
    }

}
