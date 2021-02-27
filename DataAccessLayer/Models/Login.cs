using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Login
    {
        public Login()
        {
            Users = new HashSet<User>();
        }

        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
