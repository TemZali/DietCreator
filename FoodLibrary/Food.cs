using System;
using Xamarin.Forms;

namespace FoodLibrary
{
    public class Food
    {
        public Food(int id, string name, double callories, double protein, double fat, double carbohydrate, int typeId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id;
            Callories = callories;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
            TypeId = typeId;
            FoodColor = Color.FromHex("#FFFACD");
        }

        public Color FoodColor { get; set; }

        public bool IsPicked { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public double Callories { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbohydrate { get; set; }

        public int TypeId { get; set; }

        public Food GetCopy()
        {
            Food food = new Food(Id, Name, Callories, Protein, Fat, Carbohydrate, TypeId);
            food.IsPicked = true;
            return food;
        }
    }
}
