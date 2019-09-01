using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RegisterAdd.Helpers
{
    public class UserListClickEventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}