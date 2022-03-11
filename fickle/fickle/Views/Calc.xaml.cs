using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fickle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calc : ContentPage
    {
        int currentState = 1;
        string mathOperator;
        double firstNumber, secondNumber;

        public Calc()
        {
            InitializeComponent();
            OnClear(new object(), new EventArgs());
        }

        //MÉTODOS DA CALCULADORA
        //Limpar, operações (soma, subtração, divisão, multiplicação), cálculo e Fechar (redirecioanando para página principal do app)
        void OnClear(object sender, EventArgs e) 
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            this.resultText.Text = "0";
        }
        void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            if (this.resultText.Text == "0" || currentState < 0)
            {
                this.resultText.Text = "";
                if (currentState < 0)
                    currentState *= -1;
            }
            this.resultText.Text += pressed;

            double number;
            if (double.TryParse(this.resultText.Text, out number))
            {
                this.resultText.Text = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNumber = number;
                }
                else
                {
                    secondNumber = number;
                }
            }
        }
        void OnSelectOperator(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            mathOperator = pressed;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void OnCalculate(object sender, EventArgs e)
        {
            if (currentState == 2)
            {
                Double result = 0;
                if (mathOperator == "+")
                {
                    result = firstNumber + secondNumber;
                }

                if (mathOperator == "-")
                {
                    result = firstNumber - secondNumber;
                }

                if (mathOperator == "x")
                {
                    result = firstNumber * secondNumber;
                }

                if (mathOperator == "/")
                {
                    result = firstNumber / secondNumber;
                }

                //this.resultText = result.ToString();
                this.resultText.Text = result.ToString("N0");
                firstNumber = result;
                currentState = -1;
            }
        }

    }
}