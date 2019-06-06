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
	public partial class TypeOfFoodPage : ContentPage
	{
        public string TypeTitle { get; set; }

        public List<Food> FoodList { get; set; }

        public TypeOfFoodPage(TypeOfFood type)
        {
            TypeTitle = type.TypeName;
            FoodList = new List<Food>();
            for (int i = 0; i < type.ListOfFood.Count; i++)
            {
                FoodList.Add(type.ListOfFood[i]);
            }
            InitializeComponent();
            this.BindingContext = this;

        }

        private void FoodChoice_Click(object sender, EventArgs e)
        {
            TypeListView.SelectedItem = null;
        }
    }
}