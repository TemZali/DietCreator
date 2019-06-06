using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodLibrary;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DietCreator
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "foodbase.db";
        static FoodRepository foodTable;
        public static FoodRepository FoodTable
        {
            get
            {
                if (foodTable == null)
                {
                    foodTable = new FoodRepository(DATABASE_NAME);
                }
                return foodTable;
            }
        }

        static TypeOfFoodRepository typeOfFoodTable;
        public static TypeOfFoodRepository TypeOfFoodTable
        {
            get
            {
                if (typeOfFoodTable == null)
                {
                    typeOfFoodTable = new TypeOfFoodRepository(DATABASE_NAME);
                }
                return typeOfFoodTable;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
