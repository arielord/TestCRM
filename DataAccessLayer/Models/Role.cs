using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Role
    {
        public Role()
        {
            Logins = new HashSet<Login>();
        }

        public byte RoleId { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
