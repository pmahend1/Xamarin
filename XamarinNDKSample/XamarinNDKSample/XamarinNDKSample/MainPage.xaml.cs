using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinNDKSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonGetResult_Clicked(object sender, EventArgs e)
        {
            var entryAtext = entryA.Text;
            var entryBtext = entryB.Text;

            //int a = int.TryParse(entryAtext, a);

            int a = int.TryParse(entryAtext, out a) ? a : 0;

            int b = int.TryParse(entryBtext, out b) ? b : 0;

            int res = -1;
            try
            {
                 res = XamCppLibCS.Add(a, b);
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Msg101" + e1.Message);

                Debug.WriteLine("StackTrace102 " +e1.StackTrace);


            }
           
            labelResult.Text = res.ToString();

        }
    }
}
