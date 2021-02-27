using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models;

namespace ClientProfileWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        Repository repo;
        public UserController()
        {
            repo = new Repository();
        }

        [HttpPost]
        public bool CreateLogin(Login login)
        {
            bool status = false;
            try
            {
                login.RoleId = 1;
                DataAccessLayer.Models.Login newLogin = login.JsonToEntity();
                status = repo.CreateLogin(newLogin);
                if (status)
                {
                    repo.SetUserLogin(login.userId, newLogin.LoginId);
                }
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpPost]
        public bool CreateClient(Client client)
        {
            bool status = false;
            try
            {
                DataAccessLayer.Models.Client newClient = client.JsonToEntity();
                status = repo.CreateClient(newClient);
            }
            catch (Exception) { throw; return false; }
            return status;
        }

        [HttpPut]
        public bool UpdateClient(Client client)
        {
            bool status = false;
            try
            {
                DataAccessLayer.Models.Client updatedClient = client.JsonToEntity();
                status = repo.UpdateClient(updatedClient);
            }
            catch (Exception) { return false; }
            return status;
        }

        [HttpGet]
        public JsonResult GetAllClients()
        {
            try
            {
                return Json(repo.GetAllClients());
            }
            catch (Exception) { return null; }
        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {
            try
            {
                return Json(repo.GetAllUsers());
            }
            catch (Exception) { return null; }

        }

        [HttpGet]
        public JsonResult GetUserByNameAndNumber(string first, string last, string phonenumber)
        {
            try
            {
                return Json(repo.GetUserByNameAndNumber(first, last, phonenumber));
            }
            catch (Exception) { return Json(null); }

        }

        [HttpGet]
        public JsonResult FetchUserWithLogin(string username)
        {
            try
            {
                return Json(repo.FindUserWithLogin((repo.FindLoginByUsername(username)).LoginId));
            }
            catch (Exception) { return Json(null); }
        }

        [HttpGet]
        public JsonResult GetUserDetailsById(int userId)
        {
            try
            {
                return Json(repo.GetUserDetails(userId));
            }
            catch (Exception) { return Json(null); }
        }
    }
}
