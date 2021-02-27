using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientProfileWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : Controller
    {
        Repository repo;
        public RoleController()
        {
            repo = new Repository();
        }

        [HttpGet]
        public JsonResult FetchAllRoles()
        {
            return Json(repo.GetAllRoles());
        }
    }
}