using Homebank.Core.Repositories;
using Homebank.Web.Models;
using Homebank.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Homebank.Core.Interfaces.Repositories;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class UserController : BaseController
    {
		private readonly IUserRepository _userRepository;

	    public UserController(IUserRepository userRepository)
		    : base(userRepository)
	    {
			_userRepository = userRepository;
	    }

        public ActionResult Settings()
        {
			var model = new SettingsModel {Name = HomebankUser.Name, OriginalName = HomebankUser.Name};

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

				_userRepository.Update(user);
				_userRepository.SaveChanges();

                return RedirectToAction("Index", "Home");
		    }

		    return View(model);
	    }
    }
}