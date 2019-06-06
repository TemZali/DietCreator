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
    public partial class CreatingDietPage : ContentPage
    {
        public List<TypeOfFood> ListOfTypes;

        public CreatingDietPage(List<TypeOfFood> types)
        {
            this.ListOfTypes = types;
            InitializeComponent();
        }

        private async void ActivityValueChanged(object sender, EventArgs e)
        {
            try
            {
                double ActivityValue = double.Parse(ActivityCoefficientInfo.Text);
                if (ActivityValue < 1 || ActivityValue > 2)
                {
                    throw new ApplicationException();
                }
                if (ActivityValue <= 1.2)
                {
                    ActivityInfo.Text = "Минимальный уровень физической нагрузки или полное ее отсутствие (сидячая работа, отсутствие спорта).";
                }
                else if (ActivityValue <= 1.4)
                {
                    ActivityInfo.Text = "Легкий уровень активности (легкие физические упражнения" +
                        " около 3 раз в неделю, ежедневная утренняя зарядка, пешие прогулки).";
                }
                else if (ActivityValue <= 1.6)
                {
                    ActivityInfo.Text = "Cредняя активность (спорт до 5 раз в неделю).";
                }
                else if (ActivityValue <= 1.8)
                {
                    ActivityInfo.Text = "Aктивность высокого уровня (активный образ жизни вкупе с ежедневными интенсивными тренировками).";
                }
                else
                {
                    ActivityInfo.Text = "Экстремально высокая активность " +
                        "(спортивный образ жизни, тяжелый физический труд, длительные тяжелые тренировки каждый день).";
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Введены некорректные данные о коэффициенте активности", "Ок");
            }
        }

        private async void ToGeneralChoice_Clicked(object sender, EventArgs e)
        {

            double WeightD, HighD, AgeD, CalResult;
            int count;

            if (double.TryParse(Weight.Text, out WeightD) && double.TryParse(High.Text, out HighD)&&int.TryParse(Count.Text,out count)&&count>0
                && double.TryParse(Age.Text, out AgeD) && (Picker1.SelectedIndex != -1) && (WeightD <= 300) && (HighD <= 300) && (AgeD <= 130))
            {
                if (Picker1.SelectedIndex == 0)
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD + 5) * double.Parse(ActivityCoefficientInfo.Text);
                }
                else
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD - 161) * double.Parse(ActivityCoefficientInfo.Text);
                }
                await Navigation.PushAsync(new GeneralChoice(CalResult, WeightD, ListOfTypes,count));
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите корректные данные!", "ОК");
            }
        }
    }
}