using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Login
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte RoleId { get; set; }
        public string RoleDesc { get; set; }
        public int userId { get; set; }

        public DataAccessLayer.Models.Login JsonToEntity()
        {
            DataAccessLayer.Models.Login newLogin = new DataAccessLayer.Models.Login
            {
                Username = this.Username,
                Password = this.Password,
                RoleId = this.RoleId
            };
            return newLogin;
        }
    }
}
