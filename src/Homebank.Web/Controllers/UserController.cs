using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homebank.Core.Repositories;
using Homebank.Web.Models;
using Homebank.Core.Helpers;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class UserController : BaseController
    {
		private readonly UserRepository _userRepository;

	    public UserController(UserRepository userRepository, TemplateRepository templateRepository,
		    AccountRepository accountRepository)
		    : base(userRepository, templateRepository, accountRepository)
	    {
			_userRepository = userRepository;
	    }

        public ActionResult Settings()
        {
			var model = new SettingsModel {Name = HomebankUser.Name, OriginalName = HomebankUser.Name};

			ViewBag.Header = "Settings";
	        return View(model);
        }

		[ValidateAntiForgeryToken]
		[HttpPost]
	    public ActionResult Settings(SettingsModel model)
	    {
		    if (ModelState.IsValid)
		    {
				var user = HomebankUser;
				user.Name = model.Name;

				if (!string.IsNullOrEmpty(model.NewPassword))
				{
					user.Salt = StringHelpers.RandomString(25);
					user.Password = StringHelpers.Hash(model.NewPassword + user.Salt);
				}

				_userRepository.Save(user);
				_userRepository.SaveChanges();
		    }

			ViewBag.Header = "Settings";
		    return View(model);
	    }
    }
}