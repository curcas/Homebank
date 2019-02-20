using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homebank.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult NotFound()
        {
	        ViewBag.Header = "404 - not found";
            return View();
        }
	}
}