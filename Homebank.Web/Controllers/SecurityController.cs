using System.Web.Security;
using Homebank.Entities;
using Homebank.Helpers;
using Homebank.Repositories;
using Homebank.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homebank.Web.Controllers
{
	public class SecurityController : BaseController
    {
		private readonly UserRepository _userRepository;

		public SecurityController(UserRepository userRepository, TemplateRepository templateRepository, AccountRepository accountRepository)
			: base(userRepository, templateRepository, accountRepository)
		{
			_userRepository = userRepository;
		}

        public ActionResult Register()
        {
			ViewBag.Header = "Register";
            return View(new RegisterModel());
        }

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var salt = StringHelpers.RandomString(25);
				var user = new User();

				user.Name = model.Name;
				user.Password = StringHelpers.Hash(model.Password + salt);
				user.Salt = salt;

				_userRepository.Save(user);
				_userRepository.SaveChanges();

				ViewBag.Success = "You're successfully registered. You can now log in yourself.";
			}

			ViewBag.Header = "Register";
			return View(model);
		}

		public ActionResult Login()
		{
			ViewBag.Header = "Login";
			return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Login(LoginModel model, string returnUrl)
	    {
		    if (ModelState.IsValid)
		    {
				FormsAuthentication.SetAuthCookie(_userRepository.Get(model.Name).Id.ToString(), model.RememberMe);

				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
			    {
					return Redirect(returnUrl);
			    }

			    return RedirectToAction("Overview", "Home");
		    }

			ViewBag.Header = "Login";
		    return View(model);
	    }

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}