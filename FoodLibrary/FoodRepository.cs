using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace FoodLibrary
{
    public class FoodRepository
    {
        SQLiteConnection database;
        public FoodRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
        }
        public List<Food> GetItems()
        {
            try
            {
                return database.Query<Food>("select * from foodtable;");
            }
            catch
            {
                return new List<Food>();
            }

        }
        public int Execute(string query)
        {
            return database.ExecuteScalar<int>(query);
        }
    }
}
