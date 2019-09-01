
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using RegisterAdd.Models;
using RegisterAdd.Services;
using Realms;
using Android.Content;

namespace RegisterAdd.Activities
{
    [Activity(Label = "AddUserActivity")]
    public class AddUserActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, View.IOnClickListener
    {
        private Button buttonRegisterReg;
        private EditText username, password, editTextFirstname, editTextLastname, confirmPassword;

        public void OnClick(View v)
        {
            if (v == buttonRegisterReg)
            {
                //validate username
                //
                if (editTextFirstname.Text?.Length < 3)
                {
                    Toast.MakeText(this, "Invalid first name", ToastLength.Long).Show();
                    return;
                }
                if (editTextLastname.Text?.Length < 3)
                {
                    Toast.MakeText(this, "Invalid last name", ToastLength.Long).Show();
                    return;
                }
                if (username.Text.Length > 4)
                {
                    //validate password
                    //if pass then save into db
                    //else display appropriate msg
                    try
                    {
                        var isPasswordValid = ValidationService.ValidatePassword(password.Text);
                        if (string.IsNullOrEmpty(isPasswordValid))
                        {
                            //valid
                            var realm = Realm.GetInstance();
                            var dbUser = realm.Find<User>(username.Text);
                            if (dbUser is null)
                            {
                                realm.Write(() =>
                                {
                                    var s_ = realm.Add(new User
                                    {
                                        Username = username.Text,
                                        Password = password.Text,
                                        Firstname = editTextFirstname.Text,
                                        Lastname = editTextLastname.Text,
                                        DateAdded = DateTime.UtcNow
                                    });
                                    var text_ = s_.Username + " added";
                                    Toast.MakeText(this, text_, ToastLength.Long).Show();
                                });



                                SetResult(Result.Ok);
                                Finish();
                            }
                            else
                            {
                                Toast.MakeText(this, "User " + dbUser.Username + "already exists", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            //var errorText = "Invalid password" + System.Environment.NewLine + "Password should be between of 5-12 characters length." +
                            //    System.Environment.NewLine + "Should contain at least one alpha and at least one number" + System.Environment.NewLine + "It should not have special characters" + System.Environment.NewLine + "It should not contain consecutive sequence of repeated characters.";
                            Toast.MakeText(this, isPasswordValid, ToastLength.Long).Show();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                        return;
                    }
                }
            }
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_user_layout);
            // Create your application here
            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);


            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            //drawer.AddDrawerListener(toggle);
            //toggle.SyncState();

            //NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            //navigationView.SetNavigationItemSelectedListener(this);

            editTextFirstname = FindViewById<EditText>(Resource.Id.editTextFirstnameReg);
            editTextLastname = FindViewById<EditText>(Resource.Id.editTextLastnameReg);
            username = FindViewById<EditText>(Resource.Id.editTextUsernameReg);
            password = FindViewById<EditText>(Resource.Id.editTextPasswordReg);
            confirmPassword = FindViewById<EditText>(Resource.Id.editTextPasswordRegRepeat);
            buttonRegisterReg = FindViewById<Button>(Resource.Id.buttonRegisterReg);
            buttonRegisterReg.SetOnClickListener(this);
        }
    }
}