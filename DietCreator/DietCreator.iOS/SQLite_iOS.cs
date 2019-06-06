using System;
using Xamarin.Forms;
using System.IO;
using DietCreator.iOS;


namespace DietCreator.iOS
{
    class SQLite_iOS:FoodLibrary.ISQLite
    {
        public SQLite_iOS()
        {

        }

        public string GetDatabasePath(string filename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, filename);

            return path;
        }
    }
}