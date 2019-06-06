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

        public List<LinkItem> LinkItems { get; set; }

        public StartPage(List<TypeOfFood> types)
        {
            ListOfFood = types;
            LinkItems = new List<LinkItem>()
            {
                new LinkItem("Фитнес модель за 4 месяца. Ярослав Брин","HeartImage.png","https://www.youtube.com/playlist?list=PLAjFgJlXhxRtMFyt0HDTtKHBWcp6eU8tI"),
                new LinkItem("Почему мы переедаем? Борис Цацулин","CreateDietImage.jpg","https://www.youtube.com/watch?v=zZnebdZ1Otk"),
                new LinkItem("Тренировки и питание Аквамена. Денис Семенихин","SportImage.png","https://www.youtube.com/watch?v=vG3DTOumklE")

            };
            InitializeComponent();

            this.BindingContext = this;

        }

        private void LinkClick(object sender, EventArgs e)
        {
            LinkList.SelectedItem = null;
            Device.OpenUri(new Uri(((LinkItem)((ItemTappedEventArgs)e).Item).Link));
        }

        private void NewsClick(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(((Button)sender).FontFamily));
        }
    }
}