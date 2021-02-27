using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Client
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

        public DataAccessLayer.Models.Client JsonToEntity()
        {
            DataAccessLayer.Models.Client newClient = new DataAccessLayer.Models.Client
            {
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                AssetValue = this.AssetValue,
                HomePhoneNumber = this.HomePhoneNumber,
                OfficePhoneNumber = this.OfficePhoneNumber,
                Email = this.Email,
                DriversLicenseIdNum = this.DriversLicenseIdNum,
                AccountStatusId = this.AccountStatusId,
                UserId = this.UserId
            };

            return newClient;
        }
    }
}
