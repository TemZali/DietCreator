using System;
using SQLite;
using Xamarin.Forms;

namespace FoodLibrary
{
    [Table("foodtable")]
    public class Food
    {
        public int Id { get; set; }

        [Ignore]
        public string CPFCString { get; set; }

        [Ignore]
        public bool IsPicked { get; set; }

        public string FoodName { get; set; }

        public double Callories { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbohydrate { get; set; }

        public int TypeId { get; set; }

        public Food()
        {
            IsPicked = false;
        }

        public Food(int id, string name, double callories, double protein, double fat, double carbohydrate, int typeId)
        {
            FoodName = name ?? throw new ArgumentNullException(nameof(name));
            Id = id;
            Callories = callories;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
            TypeId = typeId;
        }

        public Food GetCopy()
        {
            Food food = new Food(Id, FoodName, Callories, Protein, Fat, Carbohydrate, TypeId);
            food.IsPicked = true;
            return food;
        }
    }
}
