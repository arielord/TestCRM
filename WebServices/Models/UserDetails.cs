using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BranchOfficeLocation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string UserStatusDesc { get; set; }
        public string EntitlementDesc { get; set; }

        public DataAccessLayer.Models.UserDetails JsonToEntity()
        {
            DataAccessLayer.Models.UserDetails newUser = new DataAccessLayer.Models.UserDetails()
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                BranchOfficeLocation = this.BranchOfficeLocation,
                DateOfBirth = this.DateOfBirth,
                PhoneNumber = this.PhoneNumber,
                UserStatusDesc = this.UserStatusDesc,
                EntitlementDesc = this.EntitlementDesc
            };
            return newUser;
        }
    }
}
