using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Realms;
using RegisterAdd.Fragments;
using RegisterAdd.Helpers;
using RegisterAdd.Models;
using System.Collections.Generic;
using System.Linq;

namespace RegisterAdd
{
    public class ViewUserFragment : Fragment
    {
        private RecyclerView usersRecyclerView;
        private TextView textviewUsers;
        public List<User> Users { get; private set; }

        public static ViewUserFragment NewInstance()
        {
            var detailsFrag = new ViewUserFragment { Arguments = new Bundle() };

            return detailsFrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var realm = Realm.GetInstance();
            Users = realm.All<User>().OrderBy(x => x.DateAdded).ToList();
            var usernames = Users.Select(c => c.Firstname + " " + c.Lastname).ToList();
            var adapter = new UserListAdapter(Users);
            adapter.ItemClick += Adapter_ItemClick;

            var view = inflater.Inflate(Resource.Layout.view_users_fragment, container, false);
            textviewUsers = view.FindViewById<TextView>(Resource.Id.textviewUsers);
            usersRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerViewUsers);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(Context);
            usersRecyclerView.SetLayoutManager(mLayoutManager);
            usersRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            usersRecyclerView.SetAdapter(adapter);

            adapter.NotifyDataSetChanged();

            var token = realm.All<User>().SubscribeForNotifications((sender, changes, error) =>
            {
                Users = realm.All<User>().OrderBy(x => x.DateAdded).ToList();
                textviewUsers.Text = Users?.Count.ToString() + " Users" ?? "No users";
                var usernames1 = Users.Select(c => c.Firstname + " " + c.Lastname).ToList();
                var adapter1 = new UserListAdapter(Users);
                adapter1.ItemClick += Adapter_ItemClick;

                RecyclerView.LayoutManager mLayoutManager1 = new LinearLayoutManager(Context);
                usersRecyclerView.SetLayoutManager(mLayoutManager1);
                usersRecyclerView.SetItemAnimator(new DefaultItemAnimator());
                usersRecyclerView.SetAdapter(adapter1);

            });
            Activity.Title = "Users";
            return view;
        }

        private void Adapter_ItemClick(object sender, UserListClickEventArgs e)
        {
            var user_ = Users[e.Position];
            var fragment = ViewUserDetailsFragment.NewInstance(user_);
            Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.flContent, fragment).AddToBackStack(nameof(ViewUserDetailsFragment)).Commit();
        }
    }
}