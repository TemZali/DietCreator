using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace FoodLibrary
{
    public class TypeOfFoodRepository
    {
        SQLiteConnection database;

        public TypeOfFoodRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
        }
        public List<TypeOfFood> GetItems()
        {
            try
            {
                return database.Query<TypeOfFood>("select * from typeoffoodtable;");
            }
            catch
            {
                return new List<TypeOfFood>();
            }
        }
        public int Execute(string query)
        {
            return database.ExecuteScalar<int>(query);
        }
    }
}
