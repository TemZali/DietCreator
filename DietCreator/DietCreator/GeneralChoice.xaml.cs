using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietCreator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralChoice : ContentPage
    {
        public List<Food> Meat { get; set; }
        public List<Food> FatFood { get; set; }
        public List<Food> CarbonFood { get; set; }
        public int Count { get; set; }

        public List<TypeOfFood> TypesOfFoodList { get; set; }

        public double CalResult { get; set; }

        public double Weight { get; set; }

        public GeneralChoice(double CalResult, double Weight, List<TypeOfFood> types, int count)
        {
            Count = count;
            this.CalResult = CalResult;
            this.Weight = Weight;
            TypesOfFoodList = types;

            InitializeComponent();

            this.BindingContext = this;
        }

        public async Task<bool> CheckFood()
        {
            List<Food> PickedFood = new List<Food>();
            Meat = new List<Food>();
            FatFood = new List<Food>();
            CarbonFood = new List<Food>();
            foreach (TypeOfFood type in TypesOfFoodList)
            {
                foreach (Food food in type.ListOfFood)
                {
                    if (food.IsPicked)
                    {
                        PickedFood.Add(food.GetCopy());
                    }
                }
            }
            foreach (Food food in PickedFood)
            {
                if (food.Protein >= 9 && food.Protein > food.Carbohydrate)
                {
                    Meat.Add(food.GetCopy());
                    food.IsPicked = false;
                }
            }
            foreach (Food food in PickedFood)
            {
                if (food.Carbohydrate>50&&food.Callories<450)
                {
                    CarbonFood.Add(food.GetCopy());
                    food.IsPicked = false;
                }
            }
            foreach (Food food in PickedFood)
            {
                if (food.IsPicked)
                {
                    FatFood.Add(food.GetCopy());
                    food.IsPicked = false;
                }
            }
            bool flag = false;
            foreach(Food food in FatFood)
            {
                if (food.Fat >= 9 && food.Fat > food.Protein)
                {
                    flag = true;
                }
            }
            if (FatFood.Count == 0||!flag)
            {
                await DisplayAlert("Ошибка", "Отсутствуют продукты, содержищие жиры. Возможно, стоит добавить в список продуктов орехи, масла, жиры или колбасные изделия", "Ок");
            }
            else if (Meat.Count == 0)
            {
                await DisplayAlert("Ошибка", "Отсутствуют продукты, содержищие белки. Возможно, стоит добавить в список продуктов мясо.", "Ок");
            }
            else if (CarbonFood.Count == 0)
            {
                await DisplayAlert("Ошибка", "Отсутствуют продукты, содержищие углеводы. Возможно, стоит добавить в список продуктов крупы и макаронные изделия.", "Ок");
            }
            else
            {
                foreach (TypeOfFood type in TypesOfFoodList)
                {
                    foreach (Food food in type.ListOfFood)
                    {
                        if (food.IsPicked)
                        {
                            food.IsPicked = false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private async void CreateSave_Click(object sender, EventArgs e)
        {
            if (await CheckFood())
            {
                await Navigation.PushAsync(new FinalPage(false, Weight, Meat, FatFood, CarbonFood,Count));
            }
        }
        private async void CreateLose_Click(object sender, EventArgs e)
        {
            if (await CheckFood())
            {
                await Navigation.PushAsync(new FinalPage(true, Weight, Meat, FatFood, CarbonFood,Count));
            }
        }
        private async void TypePick(object sender, EventArgs e)
        {
            TypeOfFood type = (TypeOfFood)((ItemTappedEventArgs)e).Item;
            TypesListView.SelectedItem = null;
            await Navigation.PushAsync(new FoodPickPage(type));
        }
    }
}