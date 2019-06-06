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
	public partial class TypesOfFoodPage : ContentPage
	{
        public List<TypeOfFood> TypesOfFoodList { get; set; }

        public TypesOfFoodPage(List<TypeOfFood> types)
        {
            TypesOfFoodList = types;
            InitializeComponent();
            this.BindingContext = this;
        }
        private async void TypeChoice_Click(object sender, EventArgs e)
        {
            object item = ((ItemTappedEventArgs)e).Item;
            TypesListView.SelectedItem = null;
            await Navigation.PushAsync(new TypeOfFoodPage((TypeOfFood)(item)));
        }
    }
}