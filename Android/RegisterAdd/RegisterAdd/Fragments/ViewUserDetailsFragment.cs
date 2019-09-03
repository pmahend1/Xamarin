using Android.OS;
using Android.Views;
using Android.Widget;
using RegisterAdd.Models;

namespace RegisterAdd.Fragments
{
    public class ViewUserDetailsFragment : Android.Support.V4.App.Fragment
    {
        private User CurrentUser;
        private View baseview1, baseview2, baseview3, baseview4;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment return
            // inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.user_info, container, false);
            baseview1 = view.FindViewById(Resource.Id.base_view1);
            baseview2 = view.FindViewById(Resource.Id.base_view2);
            baseview3 = view.FindViewById(Resource.Id.base_view3);
            baseview4 = view.FindViewById(Resource.Id.base_view4);

            baseview1.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "First Name: ";
            baseview1.FindViewById<TextView>(Resource.Id.textviewText).Text = CurrentUser?.Firstname;

            baseview2.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "Last Name: ";
            baseview2.FindViewById<TextView>(Resource.Id.textviewText).Text = CurrentUser?.Lastname;

            baseview3.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "User Name: ";
            baseview3.FindViewById<TextView>(Resource.Id.textviewText).Text = CurrentUser?.Username;

            baseview4.FindViewById<TextView>(Resource.Id.textviewLabel).Text = "User since: ";
            baseview4.FindViewById<TextView>(Resource.Id.textviewText).Text = CurrentUser?.DateAdded.ToString("MM/dd/yyyy hh:mm tt") ?? "Not available";
            Activity.Title = CurrentUser?.Username + " Details" ?? "User Details";
            return view; // base.OnCreateView(inflater, container, savedInstanceState);
        }

        public static ViewUserDetailsFragment NewInstance(User user)
        {
            ViewUserDetailsFragment fragment = new ViewUserDetailsFragment();
            fragment.CurrentUser = user;
            Bundle args = new Bundle();

            //  var user = (User)args.GetSerializable("USEROBJECT");
            //args.PutString(ARG_PARAM1, param1);
            //args.putString(ARG_PARAM2, param2);
            //fragment.setArguments(args);
            return fragment;
        }
    }
}