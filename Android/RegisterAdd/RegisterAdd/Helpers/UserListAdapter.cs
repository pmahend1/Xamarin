using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace RegisterAdd.Helpers
{
    public class UserListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<UserListClickEventArgs> ItemClick;

        public event EventHandler<UserListClickEventArgs> ItemLongClick;

        private List<string> items = new List<string>();

        public UserListAdapter(List<string> data)
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

            var vh = new UserListAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as UserListAdapterViewHolder;
            var d = holder.ItemView as TextView;
            d.Text = item;

            // holder.TextView.Text = items[position];
            //holder.ItemView
        }

        public override int ItemCount => items.Count;

        private void OnClick(UserListClickEventArgs args) => ItemClick?.Invoke(this, args);

        private void OnLongClick(UserListClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

}