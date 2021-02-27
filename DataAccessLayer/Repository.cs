using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class Repository
    {
        TestCRMContext context;

        public Repository()
        {
            context = new TestCRMContext();
        }

        public UserDetails GetUserDetails(int userId)
        {
            UserDetails user = null;
            try
            {
                SqlParameter prmUserId = new SqlParameter("@UserId", userId);
                user = context.UserDetails.FromSqlRaw("SELECT * FROM ufn_FetchUserDetails(@UserId)", prmUserId).SingleOrDefault();
            }
            catch (Exception) { }
            return user;
        }

        public List<UserDetails> GetAllUserDetails()
        {
            List<UserDetails> allUsers = null;
            try
            {
                allUsers = context.UserDetails.FromSqlRaw("SELECT * FROM ufn_FetchAllUserDetails()").ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return allUsers;
        }

        public User GetUserByNameAndNumber(string first, string last, string phonenumber)
        {
            User user = null;
            try
            {
                user = context.Users.Where(u => u.FirstName == first && u.LastName == last && u.PhoneNumber == phonenumber).SingleOrDefault();
            }
            catch (Exception) { throw; }

            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = null;
            try
            {
                users = context.Users.ToList();
            }
            catch (Exception) { throw; }
            return users;
        }

        public bool UpdateUserWithDetails(UserDetails userDetails)
        {
            bool status = false;
            try
            {
                User user = context.Users.Find(userDetails.UserId);
                if (user != null)
                {
                    user.BranchOfficeId = Convert.ToInt16(context.BranchOffices.Where(b => b.BranchOfficeLocation == userDetails.BranchOfficeLocation).SingleOrDefault().BranchOfficeId);
                    user.StatusId = context.UserStatuses.Where(us => us.UserStatusDesc == userDetails.UserStatusDesc).SingleOrDefault().UserStatusId;
                    user.EntitlementId = context.Entitlements.Where(e => e.EntitlementDesc == userDetails.EntitlementDesc).SingleOrDefault().EntitlementId;
                    status = UpdateUser(user);
                }
            }
            catch (Exception) { throw; }
            return status;
        }

        public bool UpdateUser(User updatedUser)
        {
            try
            {
                User user = context.Users.Find(updatedUser.UserId);
                user.UpdateAttributes(updatedUser);
                context.Users.Update(user);
                if (context.SaveChanges() > 0) { return true; }
            }
            catch (Exception) { throw; }
            return false;
        }

        public bool CreateUserWithDetails(UserDetails userDetails)
        {
            bool status;
            try
            {
                User user = new User()
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    PhoneNumber = userDetails.PhoneNumber,
                    DateOfBirth = userDetails.DateOfBirth,
                    BranchOfficeId = Convert.ToInt16(context.BranchOffices.Where(b => b.BranchOfficeLocation == userDetails.BranchOfficeLocation).SingleOrDefault().BranchOfficeId),
                    StatusId = context.UserStatuses.Where(us => us.UserStatusDesc == userDetails.UserStatusDesc).SingleOrDefault().UserStatusId,
                    EntitlementId = context.Entitlements.Where(e => e.EntitlementDesc == userDetails.EntitlementDesc).SingleOrDefault().EntitlementId
                };
                status = CreateUser(user);
            }
            catch (Exception) { throw; }
            return status;
        }

        public bool CreateUser(User user)
        {
            try
            {
                context.Users.Add(user);
                if (context.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch (Exception) { throw; }
            return false;
        }

        public bool SetUserLogin(int userId, int loginId)
        {
            bool isSet = false;
            try
            {
                User user = context.Users.Find(userId);
                user.LoginId = loginId;
                context.Update(user);
                if (context.SaveChanges() > 0)
                {
                    isSet = true;
                }
            }
            catch (Exception) { throw; }
            return isSet;
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = null;
            try
            {
                clients = context.Clients.ToList();
            }
            catch (Exception) { clients = null; }
            return clients;
        }

        //TODO
        public List<Client> GetAllClientDetails()
        {
            return null;
        }

        public bool CreateClient(Client client)
        {
            try
            {
                context.Clients.Add(client);
                if (context.SaveChanges() > 0) { return true; }
            }
            catch (Exception) { throw; }
            return false;
        }

        public bool UpdateClient(Client updatedClient)
        {
            try
            {
                Client client = context.Clients.Find(updatedClient.ClientId);
                client.UpdateAttributes(updatedClient);
                context.Clients.Update(client);
                if (context.SaveChanges() > 0) { return true; }
            }
            catch (Exception) { throw; }
            return false;
        }

        public bool CreateLogin(Login login)
        {
            try
            {
                context.Logins.Add(login);
                if (context.SaveChanges() > 0) { return true; }
            }
            catch (Exception) { throw; }
            return false;
        }

        public bool Authenticate(string username, string password)
        {
            bool isAuthenticated = false;
            try
            {
                Login login = FindLoginByUsername(username);
                isAuthenticated = login.Password == password;
            }
            catch (Exception) { isAuthenticated = false; }
            return isAuthenticated;
        }

        public bool IsAdmin(int loginId)
        {
            return FindLoginRole(loginId) == "Admin";
        }

        public Login FindLoginByUsername(string username)
        {
            try
            {
                return context.Logins.Where(login => login.Username == username).SingleOrDefault();
            }
            catch (Exception) { throw; }
        }

        public int FindUserWithLogin(int loginId)
        {
            User user = null;
            try
            {
                user = context.Users.Where(u => u.LoginId == loginId).SingleOrDefault();
            }
            catch (Exception) { throw; }
            return user.UserId;
        }

        public string FindLoginRole(int loginId)
        {
            byte roleId = FindLoginRoleId(loginId);
            return context.Roles.Find(roleId).RoleDesc;
        }

        private byte FindLoginRoleId(int loginId)
        {
            return context.Logins.Find(loginId).RoleId;
        }

        public List<AccountStatus> GetAllAccountStatuses()
        {
            return context.AccountStatuses.ToList();
        }

        public List<BranchOffice> GetAllBranchOffices()
        {
            return context.BranchOffices.ToList();
        }

        public List<Entitlement> GetAllEntitlements()
        {
            return context.Entitlements.ToList();
        }

        public List<Role> GetAllRoles()
        {
            return context.Roles.ToList();
        }

        public List<UserStatus> GetAllUserStatuses()
        {
            return context.UserStatuses.ToList();
        }
    }
}
