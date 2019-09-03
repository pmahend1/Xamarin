using Android.App;
using Java.Lang;

namespace RegisterAdd.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        private static readonly int SPLASH_TIME_OUT = 3000;

        protected override void OnResume()
        {
            base.OnResume();
            Thread.Sleep(SPLASH_TIME_OUT);
            StartActivity(typeof(MainActivity));
        }
    }
}