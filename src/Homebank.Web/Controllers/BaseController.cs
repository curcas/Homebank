using System.Collections.Generic;
using System.Linq;
using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Homebank.Web.Controllers
{
    public class BaseController : Controller
    {
	    private readonly IUserRepository _userRepository;

	    public BaseController(IUserRepository userRepository)
	    {
		    _userRepository = userRepository;
	    }

	    public User HomebankUser
	    {
		    get
		    {
			    if (!string.IsNullOrEmpty(User.Identity.Name))
			    {
				    return _userRepository.Get(int.Parse(User.Identity.Name));
			    }

			    return null;
		    }
	    }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
		    if (!string.IsNullOrEmpty(User.Identity.Name))
		    {
                ViewBag.Username = HomebankUser.Name;
		    }

			base.OnActionExecuted(context);
	    }
    }
}
