using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Realms;
using RegisterAdd.Activities;
using RegisterAdd.Helpers;
using RegisterAdd.Models;
using System.Collections.Generic;
using System.Linq;

namespace RegisterAdd
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private RecyclerView usersRecyclerView;
        private List<User> Users = new List<User>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            AppCenter.Start("ded8578d-80fa-4d57-96a2-b4cfeb8af87f", typeof(Analytics), typeof(Crashes));

            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            usersRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewUsers);
            var realm = Realm.GetInstance();

            Users = realm.All<User>().OrderBy(x => x.DateAdded).ToList();
            var usernames = Users.Select(c => c.Firstname + " " + c.Lastname).ToList();
            var adapter = new UserListAdapter(usernames);
            adapter.ItemClick += Adapter_ItemClick;
            //adapter.OnCLi
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this);
            usersRecyclerView.SetLayoutManager(mLayoutManager);
            usersRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            usersRecyclerView.SetAdapter(adapter);

            adapter.NotifyDataSetChanged();

            var token = realm.All<User>().SubscribeForNotifications((sender, changes, error) =>
            {
                Users = realm.All<User>().OrderBy(x => x.DateAdded).ToList();
                var usernames1 = Users.Select(c => c.Firstname + " " + c.Lastname).ToList();
                var adapter1 = new UserListAdapter(usernames);
                adapter.ItemClick += Adapter_ItemClick;
                //adapter.OnCLi
                RecyclerView.LayoutManager mLayoutManager1 = new LinearLayoutManager(this);
                usersRecyclerView.SetLayoutManager(mLayoutManager1);
                usersRecyclerView.SetItemAnimator(new DefaultItemAnimator());
                usersRecyclerView.SetAdapter(adapter1);
                // Access changes.InsertedIndices, changes.DeletedIndices, and changes.ModifiedIndices

            });

        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }



        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // add user page
                Intent addUserIntent = new Intent(this, typeof(AddUserActivity));
                StartActivityForResult(addUserIntent, 200);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                //this page
            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void Adapter_ItemClick(object sender, UserListClickEventArgs e)
        {
            var pos_ = e.Position;
            Intent in_ = new Intent(this, typeof(UserInfoActivity));
            //Parcelable p = new Parcelable();
            var user_ = Users[pos_];
            in_.PutExtra("firstname", user_.Firstname);
            in_.PutExtra("lastname", user_.Lastname);
            in_.PutExtra("username", user_.Username);
            //in_.PutExtra("firstname", user_.Firstname);
            StartActivity(in_);
        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {

            }
        }
    }
}


