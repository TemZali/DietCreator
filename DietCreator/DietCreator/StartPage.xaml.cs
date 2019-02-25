using System;
using System.Collections.Generic;
using FoodLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietCreator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
        public List<TypeOfFood> ListOfFood { get; set; }

        public StartPage(List<TypeOfFood> types)
        {
            ListOfFood = types;
            InitializeComponent();

        }

        private async void CreateDiet_Click(object sendler, EventArgs e)
        {
            await Navigation.PushAsync(new CreatingDietPage(ListOfFood));
        }
    }
}