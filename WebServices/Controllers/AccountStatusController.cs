using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;

namespace WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatusController : Controller
    {
        Repository repo;
        public AccountStatusController()
        {
            repo = new Repository();
        }

        [HttpGet]
        public JsonResult FetchAllAccountStatuses()
        {
            return Json(repo.GetAllAccountStatuses());
        }
    }
}
