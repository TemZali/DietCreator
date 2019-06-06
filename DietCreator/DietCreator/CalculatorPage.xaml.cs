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

        private async void Calculate_Click(object sender, EventArgs e)
        {
            double WeightD, HighD, AgeD, CalResult;

            if (double.TryParse(Weight.Text, out WeightD) && double.TryParse(High.Text, out HighD)
                && double.TryParse(Age.Text, out AgeD) && (Picker1.SelectedIndex != -1))
            {
                if (Picker1.SelectedIndex == 0)
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD + 5) * double.Parse(ActivityCoefficientInfo.Text);
                }
                else
                {
                    CalResult = (9.9 * WeightD + 6.25 * HighD - 4.92 * AgeD - 161) * double.Parse(ActivityCoefficientInfo.Text);
                }

                SaveTitle.Text = "Диета для поддержания веса(на день)";
                SaveCallories.Text = $"Каллорий: {CalResult:f0}";
                SavePFC.Text = $"БЖУ: {WeightD:f0}/{WeightD * 1.1:f0}/{WeightD * 4:f0}г";
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите корректные данные!", "ОК");
            }
        }

        private async void ActivityValueChanged(object sender,EventArgs e)
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
                await DisplayAlert("Ошибка","Введены некорректные данные о коэффициенте активности","Ок");
            }
        }
    }
}