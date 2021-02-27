using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short BranchOfficeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public byte StatusId { get; set; }
        public byte EntitlementId { get; set; }
        public int? LoginId { get; set; }

        public DataAccessLayer.Models.User JsonToEntity()
        {
            DataAccessLayer.Models.User newUser = new DataAccessLayer.Models.User()
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                BranchOfficeId = this.BranchOfficeId,
                DateOfBirth = this.DateOfBirth,
                PhoneNumber = this.PhoneNumber,
                StatusId = this.StatusId,
                EntitlementId = this.EntitlementId
            };
            return newUser;
        }
    }
}
