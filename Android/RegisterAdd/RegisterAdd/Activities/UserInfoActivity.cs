
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace RegisterAdd.Activities
{
    [Activity(MainLauncher = false)]
    public class UserInfoActivity : AppCompatActivity
    {
        private View baseview1, baseview2, baseview3;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var firstname = Intent.GetStringExtra("firstname");
            var lastname = Intent.GetStringExtra("lastname");
            var username = Intent.GetStringExtra("username");

            SetContentView(Resource.Layout.user_info);
            baseview1 = FindViewById(Resource.Id.base_view1);
            baseview2 = FindViewById(Resource.Id.base_view2);
            baseview3 = FindViewById(Resource.Id.base_view3);

            baseview1.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "First Name: ";
            baseview1.FindViewById<TextView>(Resource.Id.textviewText).Text = firstname;



            baseview2.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "Last Name: ";
            baseview2.FindViewById<TextView>(Resource.Id.textviewText).Text = lastname;


            baseview3.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "User Name: ";
            baseview3.FindViewById<TextView>(Resource.Id.textviewText).Text = username;


            //password = FindViewById<EditText>(Resource.Id.editTextPasswordReg);
            //TextView textView = new TextView(this);
            //textView.setTextSize(45);
            //textView.setText(message);
            //setContentView(textView);
            // Create your application here
        }
    }
}