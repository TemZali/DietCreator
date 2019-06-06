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
	public partial class FoodPickPage : ContentPage
	{
        public List<Food> FoodList { get; set; }

        public string TypeTitle { get; set; }

        public FoodPickPage(TypeOfFood type)
        {
            TypeTitle = type.TypeName;
            FoodList = type.ListOfFood;

            InitializeComponent();

            this.BindingContext = this;
        }

        private void Pick(object sender, EventArgs e)
        {
            if (((Food)((ItemTappedEventArgs)e).Item).IsPicked)
            {
                ((Food)((ItemTappedEventArgs)e).Item).IsPicked = false;
            }
            else
            {
                ((Food)((ItemTappedEventArgs)e).Item).IsPicked = true;
            }
            PickListView.ItemsSource = null;
            PickListView.ItemsSource = FoodList;
        }

        private async void PickMade_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}