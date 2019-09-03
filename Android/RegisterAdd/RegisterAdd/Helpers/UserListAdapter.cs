using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using RegisterAdd.Models;
using RegisterAdd.Models.Constants;
using System;
using System.Collections.Generic;
using static Android.Views.ViewGroup;

namespace RegisterAdd.Helpers
{
    public class UserListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<UserListClickEventArgs> ItemClick;

        public event EventHandler<UserListClickEventArgs> ItemLongClick;

        private List<User> items = new List<User>();

        public UserListAdapter(List<User> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.user_item_text_view;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);
            var itemViewLL = itemView as LinearLayout;
            var lp = itemViewLL.LayoutParameters as MarginLayoutParams;
            lp.SetMargins(0, 5, 0, 10);
            itemViewLL.LayoutParameters = lp;
            var vh = new UserListAdapterViewHolder(itemViewLL, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as UserListAdapterViewHolder;
            var view_
                = holder.ItemView as View;
            // d = item;
            view_.FindViewById<TextView>(Resource.Id.textviewUsername).Text = item.Firstname + " " + item.Lastname;
            view_.FindViewById<TextView>(Resource.Id.textviewSince).Text = AppConstants.MEMBER_SINCE + item.DateAdded.ToString("MM/dd/yyyy");
            // holder.TextView.Text = items[position];
            //holder.ItemView
        }

        public override int ItemCount => items.Count;

        private void OnClick(UserListClickEventArgs args) => ItemClick?.Invoke(this, args);

        private void OnLongClick(UserListClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }
}