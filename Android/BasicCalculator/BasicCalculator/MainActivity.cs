using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;

namespace CalculatorAndroid
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView calculatorText;
        private string[] numbers = new string[2];
        private string @operator;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            calculatorText = FindViewById<TextView>(Resource.Id.calculator_text_view);

        }

        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View v)
        {
            Button button = (Button)v;
            if ("0123456789.".Contains(button.Text))
                AddDigitOrDecimalPoint(button.Text);
            else if ("÷×+-".Contains(button.Text))
                AddOperator(button.Text);
            else if ("=" == button.Text)
                Calculate();
            else if (button.Text == "Del")
                DeleteCharacter(calculatorText.Text);
            else
                Erase();
        }

        private void DeleteCharacter(string value)
        {
            //String str = calculatorText.Text;
            //str = str.Remove(str.Length - 1);
            int index = @operator == null ? 0 : 1;
            numbers[index] = numbers[index].Remove(numbers.Length-1);

            UpdateCalculatorText();


        }

        private void Erase()
        {
            numbers[0] = numbers[1] = null;
            @operator = null;
        }


        private void AddOperator(string value)
        {
            if (numbers[1] != null)
            {
                Calculate(value);
                return;
            }
            @operator = value;
            UpdateCalculatorText();



        }

        private void Calculate(string  newOperator = null)
        {
            double? result = null;
            double? first = numbers[0] == null ? null : (double?) Double.Parse(numbers[0]);
            double? second = numbers[1] == null ? null : (double?)Double.Parse(numbers[1]);

            switch (@operator)
            {
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
                case "÷":
                    result = first / second;
                    break;
                case "×":
                    result = first * second;
                    break;
            }

            if(result != null)
            {
                numbers[0] = result.ToString();
                @operator = newOperator;
                numbers[1] = null;
                UpdateCalculatorText();
            }

        }

        private void AddDigitOrDecimalPoint(string value)
        {
            int index = @operator == null ? 0 : 1;
            if (value == "." && numbers[index].Contains("."))
                return;
            
            numbers[index] += value;

            UpdateCalculatorText();


        }

        private void UpdateCalculatorText() =>  calculatorText.Text=$"{numbers[0]} {@operator} {numbers[1]}";
    
    }
}

