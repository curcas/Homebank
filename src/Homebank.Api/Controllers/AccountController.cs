using Homebank.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Homebank.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController
    {
        [HttpGet]
        public ActionResult<IList<User>> GetAll()
        {
            return new List<User>{
                new User { Id = 1, Name = "User 1" },
                new User { Id = 2, Name = "User 2" }
            };
        }
    }
}