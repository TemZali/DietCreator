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
        public int Count { get; set; }

        public List<DietItem> DietItems { get; set; }

        public FinalPage(bool flag, double Weight, List<Food> meat, List<Food> fat, List<Food> carbon, int count)
        {

            InitializeComponent();
            DietItems = new List<DietItem>();
            List<Food> Meat = meat,
                FatFood = fat,
                CarbonFood = carbon;
            Count = count;

            int StepProtein = 0, StepFat = 0, StepCarbon = 0;
            double Protein = Weight / 4, Fat = Weight * 1.1 / 4, Carbon;
            if (flag)
                Carbon = Weight / 4 * 3;
            else
                Carbon = Weight;

            Food NoFat = null;

            for (int i = 0; i < Count; i++)
            {
                DietItems.Add(new DietItem(i, ""));
                if (StepFat >= FatFood.Count)
                {
                    StepFat = 0;
                }
                while (FatFood[StepFat].Fat <= 9 || FatFood[StepFat].Fat <= FatFood[StepFat].Protein || FatFood[StepFat].Fat <= FatFood[StepFat].Carbohydrate)
                {
                    NoFat = FatFood[StepFat];
                    StepFat++;
                    if (StepFat >= FatFood.Count)
                    {
                        StepFat = 0;
                    }
                }
                if (StepProtein >= Meat.Count)
                {
                    StepProtein = 0;
                }
                if (StepCarbon >= CarbonFood.Count)
                {
                    StepCarbon = 0;
                }
                if (NoFat == null)
                {
                    Simplex simplex = new Simplex(new double[,]
                   {
                    { 1.05*Carbon,FatFood[StepFat].Carbohydrate,Meat[StepProtein].Carbohydrate,CarbonFood[StepCarbon].Carbohydrate},
                    { 1.05*Protein,FatFood[StepFat].Protein,Meat[StepProtein].Protein,CarbonFood[StepCarbon].Protein},
                    { 1.05*Fat,FatFood[StepFat].Fat,Meat[StepProtein].Fat,CarbonFood[StepCarbon].Fat},
                    {0,-FatFood[StepFat].Callories,-Meat[StepProtein].Callories,-CarbonFood[StepCarbon].Callories}
                   });
                    double[] result = new double[3];
                    simplex.Calculate(result);
                    List<Food> Meals = new List<Food>(){
                        FatFood[StepFat],
                        Meat[StepProtein],
                        CarbonFood[StepCarbon]
                    };
                    for (int j = 0; j < result.Length; j++)
                    {
                        if ((int)(result[j] * 100) != 0)
                        {
                            DietItems.Last().Info += $"{Meals[j].FoodName}: {result[j] * 100:f0}г,\n";
                        }
                    }
                    DietItems.Last().Info += $"КБЖУ: {Meals[0].Callories * result[0] + Meals[1].Callories * result[1] + Meals[2].Callories * result[2]:f0}" +
                       $"/{Meals[0].Protein * result[0] + Meals[1].Protein * result[1] + Meals[2].Protein * result[2]:f0}" +
                       $"/{Meals[0].Fat * result[0] + Meals[1].Fat * result[1] + Meals[2].Fat * result[2]:f0}" +
                       $"/{Meals[0].Carbohydrate * result[0] + Meals[1].Carbohydrate * result[1] + Meals[2].Carbohydrate * result[2]:f0}";

                }
                else
                {
                    Simplex simplex = new Simplex(new double[,]
                   {
                    { 1.05*Carbon,FatFood[StepFat].Carbohydrate,Meat[StepProtein].Carbohydrate,CarbonFood[StepCarbon].Carbohydrate,NoFat.Carbohydrate},
                    { 1.05*Protein,FatFood[StepFat].Protein,Meat[StepProtein].Protein,CarbonFood[StepCarbon].Protein,NoFat.Protein},
                    { 1.05*Fat,FatFood[StepFat].Fat,Meat[StepProtein].Fat,CarbonFood[StepCarbon].Fat,NoFat.Fat},
                    {0.5,0,0,0,1},
                    {0,-FatFood[StepFat].Callories,-Meat[StepProtein].Callories,-CarbonFood[StepCarbon].Callories,-NoFat.Callories}
                   });
                    double[] result = new double[4];
                    simplex.Calculate(result); List<Food> Meals = new List<Food>(){
                        FatFood[StepFat],
                        Meat[StepProtein],
                        CarbonFood[StepCarbon],
                        NoFat
                    };
                    for (int j = 0; j < result.Length; j++)
                    {
                        if ((int)(result[j] * 100) != 0)
                        DietItems.Last().Info += $"{Meals[j].FoodName}: {result[j] * 100:f0}г,\n";

                    }
                    DietItems.Last().Info += $"КБЖУ: {Meals[0].Callories * result[0] + Meals[1].Callories * result[1] + Meals[2].Callories * result[2] + Meals[3].Callories * result[3]:f0}" +
                        $"/{Meals[0].Protein * result[0] + Meals[1].Protein * result[1] + Meals[2].Protein * result[2] + Meals[3].Protein * result[3]:f0}" +
                        $"/{Meals[0].Fat * result[0] + Meals[1].Fat * result[1] + Meals[2].Fat * result[2] + Meals[3].Fat * result[3]:f0}" +
                        $"/{Meals[0].Carbohydrate * result[0] + Meals[1].Carbohydrate * result[1] + Meals[2].Carbohydrate * result[2] + Meals[3].Carbohydrate * result[3]:f0}";
                }
                NoFat = null;
                StepFat++;
                StepCarbon++;
                StepProtein++;
            }
            FinalListView.ItemsSource = DietItems;
        }
    }
}
/*for (int i = 0; i < Count; i++)
            {
                DietItems.Add(new DietItem(i + 1, ""));
            label:
                if (StepFat >= FatFood.Count)
                    StepFat = 0;
                if (FatFood[StepFat].Fat > 9 && FatFood[StepFat].Fat > FatFood[StepFat].Protein && FatFood[StepFat].Fat > FatFood[StepFat].Carbohydrate)
                {
                    DietItems.Last().Info += FatFood[StepFat].FoodName +
                        $": {Fat * 0.7 / FatFood[StepFat].Fat * 100:f0}г;\n";
                    ProteinNow += FatFood[StepFat].Protein * (Fat * 0.7 / FatFood[StepFat].Fat);
                    FatNow += Fat * 0.7;
                    CarbonNow += FatFood[StepFat].Carbohydrate * (Fat * 0.7 / FatFood[StepFat].Fat);
                    CalloriesNow += FatFood[StepFat].Callories * (Fat * 0.7 / FatFood[StepFat].Fat);
                    StepFat++;
                }
                else
                {
                    DietItems.Last().Info += FatFood[StepFat].FoodName +
                        $": {(CalResult - CalloriesNow) * 0.1 / FatFood[StepFat].Callories * 100:f0}г;\n";
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
                    DietItems.Last().Info += Meat[StepProtein].FoodName +
                        $": {(Protein - ProteinNow) * 0.8 / Meat[StepProtein].Protein * 100:f0}г;\n";
                    ProteinNow += (Protein - ProteinNow) * 0.8;
                    FatNow += Meat[StepProtein].Fat * ((Protein - ProteinNow) * 0.8 / Meat[StepProtein].Protein);
                    CarbonNow += Meat[StepProtein].Carbohydrate * ((Protein - ProteinNow) * 0.8 / Meat[StepProtein].Protein);
                    CalloriesNow += Meat[StepProtein].Callories * ((Protein - ProteinNow) * 0.8 / Meat[StepProtein].Protein);
                    StepProtein++;
                }
                if (CarbonNow < Carbon && CalResult > CalloriesNow)
                {
                    if (StepCarbon >= CarbonFood.Count)
                        StepCarbon = 0;
                    double a = Math.Min((Carbon - CarbonNow) / CarbonFood[StepCarbon].Carbohydrate, (CalResult - CalloriesNow) / CarbonFood[StepCarbon].Callories);
                    DietItems.Last().Info += CarbonFood[StepCarbon].FoodName + $": {a * 100:f0}г;\n";
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
                        DietItems.Last().Info += Meat[StepProtein].FoodName +
                            $": {(CalResult - CalloriesNow) / Meat[StepProtein].Callories * 100:f0}г;\n";
                        ProteinNow += Meat[StepProtein].Protein * ((CalResult - CalloriesNow) / Meat[StepProtein].Callories);
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
                            if (StepFat != FatFood.Count - 1)
                            {
                                StepFat++;
                            }
                            else
                            {
                                StepFat = 0;
                            }
                        }
                        DietItems.Last().Info += FatFood[StepFat].FoodName +
                            $": {(CalResult - CalloriesNow) / FatFood[StepFat].Callories * 100:f0}г;\n";
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
            
            DietItems.Add(new DietItem(0, $"Средний каллораж за прием пищи:{CalTotal / Count:f0}\n" +
                $"Среднее кол-во белков за прием пищи:{ProtTotal / Count:f0}\n" +
                $"Среднее кол-во жиров за прием пищи:{FatTotal / Count:f0}\n" +
                $"Среднее кол-во углеводов за прием пищи:{CarbTotal / Count:f0}."));*/
