using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabapp.Services;
using tabapp.Views;

namespace tabapp
{
    public partial class App : Application
    {
        public static NavigationPage Navigation = null;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            Navigation = new NavigationPage(new MainPage());
            Application.Current.MainPage = Navigation;
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        // Called by the back button in our header/navigation bar.
        public async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
