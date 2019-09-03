using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RegisterAdd.Models;
using System;
using System.Collections.Generic;
using Debug = System.Diagnostics.Debug;
using Fragment = Android.Support.V4.App.Fragment;

namespace RegisterAdd
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private List<User> Users = new List<User>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            AppCenter.Start("ded8578d-80fa-4d57-96a2-b4cfeb8af87f", typeof(Analytics), typeof(Crashes));

            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            if (savedInstanceState == null)
            {
                try
                {
                   var fragment = ViewUserFragment.NewInstance();
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.flContent, fragment).Commit();
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
        
            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
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

            DrawerLayout mainLayout = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);

            if (id == Resource.Id.nav_camera)
            {
                try
                {
                    var fragment = AddUserFragment.NewInstance();
                    SupportFragmentManager.BeginTransaction().
                        Replace(Resource.Id.flContent, fragment).
                        AddToBackStack(nameof(AddUserFragment)).Commit();
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (id == Resource.Id.nav_gallery)
            {
                var fragment = ViewUserFragment.NewInstance();
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.flContent, fragment).Commit();
            }
            else
            {
                //fr = ViewUserFragment.NewInstance(0);
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
    }
}