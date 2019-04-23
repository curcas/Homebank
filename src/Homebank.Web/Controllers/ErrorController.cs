using Homebank.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IUserRepository userRepository)
            : base(userRepository)
        {           
        }

        [Route("Error/404")]
        public IActionResult Error404()
        {
            ViewBag.TraceIdentifier = HttpContext.TraceIdentifier;

            return View();
        }

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            ViewBag.TraceIdentifier = HttpContext.TraceIdentifier;

            return View();
        }
    }
}