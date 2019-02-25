using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FoodLibrary
{
    public class TypeOfFood
    {
        public Color TypeColor { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Food> ListOfFood { get; set; }
    }
}
