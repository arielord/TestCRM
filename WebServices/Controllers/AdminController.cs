using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebServices.Models;

namespace ClientProfileWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : Controller
    {
        Repository repo;
        public AdminController()
        {
            repo = new Repository();
        }

        [HttpGet]
        public JsonResult FetchUserDetails(int userId)
        {
            try
            {
                return Json(repo.GetUserDetails(userId));
            }
            catch (Exception) { return Json(null); }
        }

        [HttpGet]
        public JsonResult FetchAllUserDetails()
        {
            try
            {
                return Json(repo.GetAllUserDetails());
            }
            catch (Exception) { return Json(null); }
        }

        [HttpPost]
        public bool CreateUser(UserDetails user)
        {
            bool status = false;
            try
            {
                DataAccessLayer.Models.UserDetails newUser = user.JsonToEntity();
                status = repo.CreateUserWithDetails(newUser);
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpPut]
        public bool UpdateUser(UserDetails user)
        {
            bool status = false;
            try
            {
                DataAccessLayer.Models.UserDetails updatedUser = user.JsonToEntity();
                status = repo.UpdateUserWithDetails(updatedUser);
            }
            catch (Exception) { status = false; }
            return status;
        }
    }
}