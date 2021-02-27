using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            Clients = new HashSet<Client>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short BranchOfficeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public byte StatusId { get; set; }
        public byte EntitlementId { get; set; }
        public int? LoginId { get; set; }

        public virtual Login Login { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public void UpdateAttributes(User user)
        {
            this.BranchOfficeId = user.BranchOfficeId;
            this.StatusId = user.StatusId;
            this.EntitlementId = user.EntitlementId;
        }
    }
}
