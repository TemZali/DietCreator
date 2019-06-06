using System;
using System.IO;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DietCreator.Droid;


[assembly: Dependency(typeof(SQLite_Android))]
namespace DietCreator.Droid
{
    class SQLite_Android:FoodLibrary.ISQLite
    {
        public SQLite_Android()
        {

        }
        public string GetDatabasePath(string filename)
        {
            string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(DocumentsPath,filename);
            return path;
        }
    }
}