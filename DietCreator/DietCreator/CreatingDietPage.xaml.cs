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

        private void StepActivity(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue >= 1.0 && e.NewValue <= 2.0)
            {
                ActivityCoefficientInfo.Text = "Коэффициент активности равен:" + e.NewValue.ToString();
            }
            switch ((int)(10 * e.NewValue))
            {
                case 10:
                case 11:
                case 12:
                    ActivityInfo1.Text = "Минимальный уровень физической нагрузки или полное ее отсутствие (сидячая работа, отсутствие спорта).";
                    break;
                case 13:
                case 14:
                    ActivityInfo1.Text = "Легкий уровень активности (легкие физические упражнения" +
                        " около 3 раз в неделю, ежедневная утренняя зарядка, пешие прогулки).";
                    break;
                case 15:
                case 16:
                    ActivityInfo1.Text = "Cредняя активность (спорт до 5 раз в неделю).";
                    break;
                case 17:
                case 18:
                    ActivityInfo1.Text = "Aктивность высокого уровня (активный образ жизни вкупе с ежедневными интенсивными тренировками).";
                    break;
                case 19:
                case 20:
                    ActivityInfo1.Text = "Экстремально высокая активность " +
                        "(спортивный образ жизни, тяжелый физический труд, длительные тяжелые тренировки каждый день).";
                    break;
            }
        }

        private async void ToGeneralChoice_Clicked(object sender, EventArgs e)
        {

            double WeightD, HighD, AgeD, CalResult;

            if (double.TryParse(Weight.Text, out WeightD) && double.TryParse(High.Text, out HighD)
                && double.TryParse(Age.Text, out AgeD) && (Picker1.SelectedIndex != -1) && (WeightD <= 300) && (HighD <= 300) && (AgeD <= 130))
            {
                if (Picker1.SelectedIndex == 0)
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD + 5) * ActivityStepper.Value;
                }
                else
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD - 161) * ActivityStepper.Value;
                }
                await Navigation.PushAsync(new GeneralChoice(CalResult, WeightD, ListOfTypes));
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите корректные данные!", "ОК");
            }
        }
    }
}