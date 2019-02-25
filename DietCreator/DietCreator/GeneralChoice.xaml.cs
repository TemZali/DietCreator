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

        public List<TypeOfFood> TypesOfFoodList { get; set; }

        public double CalResult { get; set; }

        public double Weight { get; set; }

        public GeneralChoice(double CalResult, double Weight, List<TypeOfFood> types)
        {
            this.CalResult = CalResult;
            this.Weight = Weight;
            TypesOfFoodList = types;

            InitializeComponent();

            this.BindingContext = this;
        }

        public async Task<bool> CheckFood(double CalResult)
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
                if (food.Protein >= 9 && food.Protein > food.Fat && food.Protein > food.Carbohydrate)
                {
                    Meat.Add(food.GetCopy());
                    food.IsPicked = false;
                }
            }
            foreach (Food food in PickedFood)
            {
                if (food.Carbohydrate >= 35 && food.Carbohydrate <= 74 && food.Callories <= 352)
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
            if (FatFood.Count == 0)
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
                    type.TypeColor = Color.FromHex("#FFFACD");
                    foreach (Food food in type.ListOfFood)
                    {
                        if (food.IsPicked)
                        {
                            food.IsPicked = false;
                            food.FoodColor = Color.FromHex("#FFFACD");
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private async void CreateSave_Click(object sender, EventArgs e)
        {
            if (await CheckFood(CalResult / 4))
            {
                await Navigation.PushAsync(new FinalPage(CalResult / 4, Weight, Meat, FatFood, CarbonFood));
            }
        }
        private async void CreateLose_Click(object sender, EventArgs e)
        {
            if (await CheckFood(CalResult * 0.2))
            {
                await Navigation.PushAsync(new FinalPage(CalResult * 0.2, Weight, Meat, FatFood, CarbonFood));
            }
        }
        private async void TypePick(object sender, EventArgs e)
        {
            ((TypeOfFood)((ItemTappedEventArgs)e).Item).TypeColor = Color.Green;
            TypesListView.ItemsSource = null;
            TypesListView.ItemsSource = TypesOfFoodList;
            await Navigation.PushAsync(new FoodPickPage((TypeOfFood)((ItemTappedEventArgs)e).Item));
        }
    }
}