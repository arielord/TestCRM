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
    public class BranchOfficeController : Controller
    {
        Repository repo;
        public BranchOfficeController()
        {
            repo = new Repository();
        }

        [HttpGet]
        public JsonResult FetchAllBranchOffices()
        {
            return Json(repo.GetAllBranchOffices());
        }
    }
}