using System.Collections.Generic;
using Homebank.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Api.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController
    {
        /// <summary>
        /// Returns a list of all existing users.
        /// </summary>
        /// <returns>List of <![CDATA[<see cref="Homebank.Api.Models.User"/>]]>.</returns>
        [HttpGet]
        public ActionResult<IList<User>> GetAll()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "User 1" },
                new User { Id = 2, Name = "User 2" },
            };
        }
    }
}