using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AssetValue { get; set; }
        public string HomePhoneNumber { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string Email { get; set; }
        public string DriversLicenseIdNum { get; set; }
        public int UserId { get; set; }
        public byte AccountStatusId { get; set; }

        public virtual AccountStatus AccountStatus { get; set; }
        public virtual User User { get; set; }

        public void UpdateAttributes(Client client)
        {
            this.AssetValue = client.AssetValue;
            this.UserId = client.UserId;
            this.AccountStatusId = client.AccountStatusId;
        }
    }
}
