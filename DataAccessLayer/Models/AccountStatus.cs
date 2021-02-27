using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class AccountStatus
    {
        public AccountStatus()
        {
            Clients = new HashSet<Client>();
        }

        public byte AccountStatusId { get; set; }
        public string AccountStatusDesc { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
