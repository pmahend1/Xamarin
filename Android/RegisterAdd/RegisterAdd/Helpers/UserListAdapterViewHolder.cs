using Android.Support.V7.Widget;
using Android.Views;
using System;

namespace RegisterAdd.Helpers
{
    public class UserListAdapterViewHolder : RecyclerView.ViewHolder
    {
        public UserListAdapterViewHolder(View itemView, Action<UserListClickEventArgs> clickListener,
                             Action<UserListClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new UserListClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new UserListClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}