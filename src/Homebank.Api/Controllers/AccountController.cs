using System.Collections.Generic;
using System.Linq;
using Homebank.Api.Database;
using Homebank.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Api.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController
    {
        private readonly HomebankContext homebankContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="homebankContext">The DB context for Homebank.</param>
        public AccountController(HomebankContext homebankContext)
        {
            this.homebankContext = homebankContext;
        }

        /// <summary>
        /// Returns a list of all existing users.
        /// </summary>
        /// <returns>List of <![CDATA[<see cref="Homebank.Api.Models.User"/>]]>.</returns>
        [HttpGet]
        public ActionResult<IList<User>> GetAll()
        {
            return homebankContext.Users
                .Select(u => new User { Id = u.Id, Name = u.Name })
                .ToList();
        }
    }
}