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
    public class LoginController : Controller
    {
        Repository repo;
        public LoginController()
        {
            repo = new Repository();
        }

        [HttpGet]
        public bool IsAuthenticated(string username, string password)
        {
            return repo.Authenticate(username, password);
        }

        [HttpGet]
        public bool IsAdmin(string username)
        {
            bool isAdmin = false;
            try
            {
                isAdmin = repo.IsAdmin(repo.FindLoginByUsername(username).LoginId);
            }
            catch (Exception) { isAdmin = false; }
            return isAdmin;
        }

        [HttpGet]
        public bool AuthenticateAdmin(string username, string password)
        {
            bool authenticated = false;
            try
            {
                authenticated = repo.Authenticate(username, password) && repo.IsAdmin(repo.FindLoginByUsername(username).LoginId);
            }
            catch (Exception) { authenticated = false; }
            return false;
        }

        [HttpGet]
        public JsonResult FetchLoginIfAuthenticated(string username, string password)
        {
            try
            {
                if (IsAuthenticated(username, password))
                {
                    return Json(repo.FindLoginByUsername(username));
                }
            }
            catch (Exception) { return Json(null); }
            return Json(null);
        }

        [HttpGet]
        public JsonResult FetchUserWithLogin(string username)
        {
            try
            {
                return Json(repo.GetUserDetails(repo.FindUserWithLogin(repo.FindLoginByUsername(username).LoginId)));
            }
            catch (Exception) { return Json(null); }
        }
    }
}