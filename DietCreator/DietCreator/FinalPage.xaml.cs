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
	public partial class FinalPage : ContentPage
	{
        //Random random = new Random();

        public FinalPage(double CalResult, double Weight, List<Food> meat, List<Food> fat, List<Food> carbon)
        {
            /*foreach (TypeOfFood type in types)
            {
                foreach (Food food in type.ListOfFood)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        food.IsPicked = false;
                    }
                    else
                    {
                        food.IsPicked = true;
                    }
                }
            }*/
            InitializeComponent();
            List<Food> Meat = meat,
                FatFood = fat,
                CarbonFood = carbon;

            int StepProtein = 0, StepFat = 0, StepCarbon = 0;
            double Protein = Weight / 4, Fat = Weight * 1.1 / 4, Carbon = Weight, ProteinNow = 0, FatNow = 0, CarbonNow = 0, CalloriesNow = 0,
                CalTotal = 0, ProtTotal = 0, FatTotal = 0, CarbTotal = 0;
            for (int i = 1; i <= 28; i++)
            {
                FinalLayout.Children.Add(new Label { Text = $"День {(i + 3) / 4}:", TextColor = Color.Red });
                FinalLayout.Children.Add(new Label { Text = $"Прием пищи {i}:", TextColor = Color.Blue });
            //FinalLabel.Text += $"День {(i + 3) / 4}:\nПрием пищи {i}:\n";
            label:
                if (StepFat >= FatFood.Count)
                    StepFat = 0;
                if (FatFood[StepFat].Fat > 9 && FatFood[StepFat].Fat > FatFood[StepFat].Protein && FatFood[StepFat].Fat > FatFood[StepFat].Carbohydrate)
                {
                    FinalLayout.Children.Add(new Label
                    {
                        Text = FatFood[StepFat].Name +
                        $": {Fat * 0.7 / FatFood[StepFat].Fat * 100:f1}г;"
                    });
                    ProteinNow += FatFood[StepFat].Protein * (Fat * 0.7 / FatFood[StepFat].Fat);
                    FatNow += Fat * 0.7;
                    CarbonNow += FatFood[StepFat].Carbohydrate * (Fat * 0.7 / FatFood[StepFat].Fat);
                    CalloriesNow += FatFood[StepFat].Callories * (Fat * 0.7 / FatFood[StepFat].Fat);
                    StepFat++;
                }
                else
                {
                    FinalLayout.Children.Add(new Label
                    {
                        Text = FatFood[StepFat].Name +
                        $": {(CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories * 100:f1}г;"
                    });
                    ProteinNow += FatFood[StepFat].Protein * ((CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories);
                    FatNow += FatFood[StepFat].Fat * ((CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories);
                    CarbonNow += FatFood[StepFat].Carbohydrate * ((CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories);
                    CalloriesNow += FatFood[StepFat].Callories * ((CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories);
                    StepFat++;
                    goto label;
                }
                if (ProteinNow < Protein && CalloriesNow < CalResult)
                {
                    if (StepProtein >= Meat.Count)
                        StepProtein = 0;
                    FinalLayout.Children.Add(new Label
                    {
                        Text = Meat[StepProtein].Name +
                        $": {(Protein - ProteinNow) * 0.4 / Meat[StepProtein].Protein * 100:f1}г;"
                    });
                    ProteinNow = Protein * 0.4;
                    FatNow += Meat[StepProtein].Fat * ((Protein - ProteinNow) * 0.4 / Meat[StepProtein].Protein);
                    CarbonNow += Meat[StepProtein].Carbohydrate * ((Protein - ProteinNow) * 0.4 / Meat[StepProtein].Protein);
                    CalloriesNow += Meat[StepProtein].Callories * ((Protein - ProteinNow) * 0.4 / Meat[StepProtein].Protein);
                    StepProtein++;
                }
                if (CarbonNow < Carbon && CalResult > CalloriesNow)
                {
                    if (StepCarbon >= CarbonFood.Count)
                        StepCarbon = 0;
                    double a = Math.Min((Carbon - CarbonNow) / CarbonFood[StepCarbon].Carbohydrate, (CalResult - CalloriesNow) / CarbonFood[StepCarbon].Callories);
                    FinalLayout.Children.Add(new Label { Text = CarbonFood[StepCarbon].Name + $": {a * 100:f1}г;" });
                    ProteinNow += CarbonFood[StepCarbon].Protein * a;
                    FatNow += CarbonFood[StepCarbon].Fat * a;
                    CarbonNow += CarbonFood[StepCarbon].Carbohydrate * a;
                    CalloriesNow += CarbonFood[StepCarbon].Callories * a;
                    StepCarbon++;
                }
                if (CalResult > CalloriesNow)
                {
                    if (Protein - ProteinNow > Fat - FatNow)
                    {

                        if (StepProtein >= Meat.Count)
                            StepProtein = 0;
                        FinalLayout.Children.Add(new Label
                        {
                            Text = Meat[StepProtein].Name +
                            $": {(CalResult - CalloriesNow) / Meat[StepProtein].Callories * 100:f1}г;"
                        });
                        ProteinNow = Meat[StepProtein].Protein * ((CalResult - CalloriesNow) / Meat[StepProtein].Callories);
                        FatNow += Meat[StepProtein].Fat * ((CalResult - CalloriesNow) / Meat[StepProtein].Callories);
                        CarbonNow += Meat[StepProtein].Carbohydrate * ((CalResult - CalloriesNow) / Meat[StepProtein].Callories);
                        CalloriesNow += Meat[StepProtein].Callories * ((CalResult - CalloriesNow) / Meat[StepProtein].Callories);
                        StepProtein++;

                    }
                    else
                    {
                        while (FatFood[StepFat].Fat > 9 && FatFood[StepFat].Fat > FatFood[StepFat].Protein && FatFood[StepFat].Fat > FatFood[StepFat].Carbohydrate)
                        {
                            if (StepFat >= FatFood.Count)
                                StepFat = 0;
                            StepFat++;
                        }
                        FinalLayout.Children.Add(new Label
                        {
                            Text = FatFood[StepFat].Name +
                            $": {(CalResult - CalloriesNow) / FatFood[StepFat].Callories * 100:f1}г;"
                        });
                        ProteinNow += FatFood[StepFat].Protein * ((CalResult - CalloriesNow) / FatFood[StepFat].Callories);
                        FatNow += FatFood[StepFat].Fat * ((CalResult - CalloriesNow) / FatFood[StepFat].Callories);
                        CarbonNow += FatFood[StepFat].Carbohydrate * ((CalResult - CalloriesNow) / FatFood[StepFat].Callories);
                        CalloriesNow += FatFood[StepFat].Callories * ((CalResult - CalloriesNow) / FatFood[StepFat].Callories);
                        StepFat++;
                    }
                }
                CalTotal += CalloriesNow;
                ProtTotal += ProteinNow;
                FatTotal += FatNow;
                CarbTotal += CarbonNow;
                CalloriesNow = 0;
                ProteinNow = 0;
                FatNow = 0;
                CarbonNow = 0;
            }
            FinalLayout.Children.Add(new Label
            {
                Text = $"Средний каллораж в день:{CalTotal / 7:f1}\n" +
                $"Среднее кол-во белков в день:{ProtTotal / 7:f1}\n" +
                $"Среднее кол-во жиров в день:{FatTotal / 7:f1}\n" +
                $"Среднее кол-во углеводов в день:{CarbTotal / 7:f1}."
            ,
                TextColor = Color.Red
            });
        }
        private async void EndClick(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}