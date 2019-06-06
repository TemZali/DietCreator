using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;

namespace FoodLibrary
{
    [Table("typeoffoodtable")]
    public class TypeOfFood
    {
        public TypeOfFood()
        {
            ListOfFood = new List<Food>();
        }

        [PrimaryKey,AutoIncrement,Column("_id")]
        public int Id { get; set; }

        public string TypeImage { get; set; }

        public string TypeName { get; set; }

        [Ignore]
        public List<Food> ListOfFood { get; set; }
    }
}
