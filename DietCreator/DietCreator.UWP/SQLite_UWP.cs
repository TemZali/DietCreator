using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using DietCreator.UWP;

namespace DietCreator.UWP
{
    public class SQLite_UWP : FoodLibrary.ISQLite
    {
        public SQLite_UWP() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }
    }
}
