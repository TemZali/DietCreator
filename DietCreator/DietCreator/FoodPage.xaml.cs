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
	public partial class FoodPage : ContentPage
	{
        public FoodPage(Food food)
        {
            InitializeComponent();
            Title = food.Name;
            CalloriesLabel.Text = "Каллории:" + food.Callories.ToString();
            ProteinLabel.Text = "Белки:" + food.Protein.ToString();
            FatLabel.Text = "Жиры:" + food.Fat.ToString();
            CarbohydrateLabel.Text = "Углеводы:" + food.Carbohydrate.ToString();
        }
    }
}