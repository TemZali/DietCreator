using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietCreator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }
        private void StepActivity(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue >= 1.0 && e.NewValue <= 2.0)
            {
                ActivityCoefficientInfo.Text = "Коэффициент активности равен:" + e.NewValue.ToString();
            }
            switch ((int)(10 * ActivityStepper.Value))
            {
                case 10:
                case 11:
                case 12:
                    ActivityInfo.Text = "Минимальный уровень физической нагрузки или полное ее отсутствие (сидячая работа, отсутствие спорта).";
                    break;
                case 13:
                case 14:
                    ActivityInfo.Text = "Легкий уровень активности (легкие физические упражнения" +
                        " около 3 раз в неделю, ежедневная утренняя зарядка, пешие прогулки).";
                    break;
                case 15:
                case 16:
                    ActivityInfo.Text = "Cредняя активность (спорт до 5 раз в неделю).";
                    break;
                case 17:
                case 18:
                    ActivityInfo.Text = "Aктивность высокого уровня (активный образ жизни вкупе с ежедневными интенсивными тренировками).";
                    break;
                case 19:
                case 20:
                    ActivityInfo.Text = "Экстремально высокая активность " +
                        "(спортивный образ жизни, тяжелый физический труд, длительные тяжелые тренировки каждый день).";
                    break;
            }
        }
        private async void Calculate_Click(object sender, EventArgs e)
        {
            double WeightD, HighD, AgeD, CalResult;

            if (double.TryParse(Weight.Text, out WeightD) && double.TryParse(High.Text, out HighD)
                && double.TryParse(Age.Text, out AgeD) && (Picker1.SelectedIndex != -1))
            {
                if (Picker1.SelectedIndex == 0)
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD + 5) * ActivityStepper.Value;
                }
                else
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD - 161) * ActivityStepper.Value;
                }
                await DisplayAlert("Итог", $"Ежедневный расход калорий:{(int)CalResult}\n" +
                    $"Необходимое количество белков:{WeightD}г\n" +
                    $"Количество жиров:{1.1 * WeightD}г\n" +
                    $"Количество углеводов:{4 * WeightD}г.", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите корректные данные!", "ОК");
            }
        }
    }
}