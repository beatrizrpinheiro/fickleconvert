using fickle.API;
using fickle.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fickle
{
    public partial class MainPage : ContentPage
    {
        public string currentValue { get; set; }
        public string currentCurrency { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e) //Direcionamento para calculadora em uma Navigation Page
        {
            await Navigation.PushAsync(new Calc());
        }

        private void GetCurrentValue(object sender, TextChangedEventArgs e)
        {
            this.currentValue = e.NewTextValue;
            var entry = (Entry)sender;
            this.currentCurrency = entry.ClassId;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentValue)) //Caso o usuário não digitar valor em nenhum dos campos
            {
                await DisplayAlert("Atenção!", "Informe um valor em um dos campos!", "Ok");

                return;
            }

            var result = await ApiService.GetConvert(currentValue, currentCurrency); //setar valor atual informado para atualização apenas dos demais não informados

            if (currentCurrency != "BRL")
                this.EntryMoedaBR.Text = "R$ " + decimal.Parse(result.data.BRL.value.ToString()).ToString("n2");

            if (currentCurrency != "USD")
                this.EntryMoedaEUA.Text = "$ " + decimal.Parse(result.data.USD.value.ToString()).ToString("n2");

            if (currentCurrency != "EUR")
                this.EntryMoedaEURO.Text = "Є " + decimal.Parse(result.data.EUR.value.ToString()).ToString("n2");

            if (currentCurrency != "MXN")
                this.EntryMoedaPESOMEX.Text = "Mex$ " + decimal.Parse(result.data.MXN.value.ToString()).ToString("n2");

            if (currentCurrency != "CAD")
                this.EntryMoedaCAD.Text = "C$ " + decimal.Parse(result.data.CAD.value.ToString()).ToString("n2");
        }
    }
}
