using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Realms;
using RegisterAdd.Models;
using RegisterAdd.Models.Constants;
using RegisterAdd.Services;
using System;

namespace RegisterAdd
{
    public class AddUserFragment : Fragment, View.IOnClickListener
    {
        private Button buttonRegisterReg;
        private EditText username, password, editTextFirstname, editTextLastname, confirmPassword;

        public static AddUserFragment NewInstance()
        {
            AddUserFragment fragment = new AddUserFragment();
            Bundle args = new Bundle();
            return fragment;
        }

        public void OnClick(View v)
        {
            if (v == buttonRegisterReg)
            {
                //validate username
                var usernameValMsg = ValidationService.ValidateUsername(username.Text);
                if (!string.IsNullOrEmpty(usernameValMsg))
                {
                    Toast.MakeText(Context, usernameValMsg, ToastLength.Long).Show();
                    return;
                }
                var isFirstnameEmpty = ValidationService.IsEmpty(editTextFirstname.Text);
                if (isFirstnameEmpty)
                {
                    Toast.MakeText(Context, "Firstname is empty", ToastLength.Long).Show();
                    return;
                }

                var isLastnameEmpty = ValidationService.IsEmpty(editTextLastname.Text);
                if (isLastnameEmpty)
                {
                    Toast.MakeText(Context, "Lastname is empty", ToastLength.Long).Show();
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
                                    Toast.MakeText(Context, text_, ToastLength.Long).Show();
                                });

                                Activity.SupportFragmentManager.PopBackStack();
                            }
                            else
                            {
                                Toast.MakeText(Context, "User " + dbUser.Username + " already exists", ToastLength.Long).Show();
                            }
                        }
                        else
                        {
                            Toast.MakeText(Context, isPasswordValid, ToastLength.Long).Show();
                            return;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
                        return;
                    }
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.add_user_fragment, container, false);
            editTextFirstname = view.FindViewById<EditText>(Resource.Id.editTextFirstnameReg);
            editTextLastname = view.FindViewById<EditText>(Resource.Id.editTextLastnameReg);
            username = view.FindViewById<EditText>(Resource.Id.editTextUsernameReg);
            password = view.FindViewById<EditText>(Resource.Id.editTextPasswordReg);
            confirmPassword = view.FindViewById<EditText>(Resource.Id.editTextPasswordRegRepeat);
            buttonRegisterReg = view.FindViewById<Button>(Resource.Id.buttonRegisterReg);
            buttonRegisterReg.SetOnClickListener(this);

            username.TextChanged += Username_TextChanged;
            password.TextChanged += Password_TextChanged;
            confirmPassword.TextChanged += ConfirmPassword_TextChanged;
            Activity.Title = "Add User";
            return view;
        }

        private void Username_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            var message = ValidationService.ValidateUsername(e.Text.ToString());
            if (!string.IsNullOrEmpty(message))
            {
                var icon = Activity.GetDrawable(Resource.Drawable.alert_circle_outline);
                username.SetError(message, icon);
            }
        }

        private void ConfirmPassword_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var passwordText = password.Text;
            if (!Equals(passwordText, e.Text.ToString()))
            {
                confirmPassword.SetError(AppConstants.PASSWORDS_DONT_MATCH, Activity.GetDrawable(Resource.Drawable.alert_circle_outline));
            }
        }

        private void Password_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            var message = ValidationService.ValidatePassword(e.Text.ToString());
            if (!string.IsNullOrEmpty(message))
            {
                var icon = Activity.GetDrawable(Resource.Drawable.alert_circle_outline);
                password.SetError(message, icon);
            }
        }
    }
}