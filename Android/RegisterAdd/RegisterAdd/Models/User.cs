using Realms;
using System;

namespace RegisterAdd.Models
{
    public class User : RealmObject
    {
        [PrimaryKey]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTimeOffset DateAdded { get; set; }
    }
}