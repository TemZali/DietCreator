using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
