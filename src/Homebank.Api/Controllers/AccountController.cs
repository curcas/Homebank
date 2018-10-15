using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Controllers
{
    [Route("api/[controller]")]
    public class AccountController
    {
        [HttpGet]
        public ActionResult<IList<string>> GetAll()
        {
            return new List<string>();
        }
    }
}