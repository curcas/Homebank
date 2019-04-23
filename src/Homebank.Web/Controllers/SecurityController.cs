using Homebank.Core.Entities;
using Homebank.Core.Helpers;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Homebank.Web.Controllers
{
	public class SecurityController : BaseController
    {
		private readonly IUserRepository _userRepository;

		public SecurityController(IUserRepository userRepository)
			: base(userRepository)
		{
			_userRepository = userRepository;
		}

        public ActionResult Register()
        {
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

				_userRepository.Add(user);
				_userRepository.SaveChanges();

                return RedirectToAction("Login", "Security");
			}

			return View(model);
		}

		public ActionResult Login(string returnUrl)
		{
			return View(new LoginModel {
                ReturnUrl = returnUrl
            });
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
	    {
		    if (ModelState.IsValid)
		    {
                var user = _userRepository.Get(model.Name);

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
			    {
					return Redirect(returnUrl);
			    }

			    return RedirectToAction("Index", "Home");
		    }

            model.ReturnUrl = returnUrl;
		    return View(model);
	    }

		public async Task<ActionResult> Logout()
		{
            await HttpContext.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}