using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using FoodLibrary;

namespace DietCreator
{
    public partial class MainPage : MasterDetailPage
    {
        public List<TypeOfFood> ListOfTypes { get; set; }

        public HttpClient client { get; set; }

        public MainPage()
        {
            InitializeComponent();

            client = new HttpClient();

            client.BaseAddress = new Uri("https://fooddietapi.azurewebsites.net");

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var stop = Task.Run(GetInfo);

            stop.Wait();

            Detail = new NavigationPage(new StartPage(ListOfTypes));
        }

        private async Task GetInfo()
        {
            var uri = new Uri(string.Format("https://fooddietapi.azurewebsites.net/api/typeoffood", string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ListOfTypes = JsonConvert.DeserializeObject<List<TypeOfFood>>(content);
                    foreach (TypeOfFood type in ListOfTypes)
                    {
                        type.TypeColor = Color.FromHex("#FFFACD");
                        foreach (Food food in type.ListOfFood)
                        {
                            food.FoodColor = Color.FromHex("#FFFACD");
                        }
                    }
                }
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Отсутствует подключение к сети", "Ок");
            }
        }

        private void CreateDiet_Click(object sendler, EventArgs e)
        {
            Detail = new NavigationPage(new CreatingDietPage(ListOfTypes));
        }
        private void Calculator_Click(object sendler, EventArgs e)
        {
            Detail = new NavigationPage(new CalculatorPage());
        }
        private void SearchService_Click(object sendler, EventArgs e)
        {
            Detail = new NavigationPage(new TypesOfFoodPage(ListOfTypes));
        }
        private void AboutProgram_Click(object sendler, EventArgs e)
        {

        }
        private void Settings_Click(object sendler, EventArgs e)
        {

        }
    }
}
