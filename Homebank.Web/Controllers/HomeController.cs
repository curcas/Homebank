using Homebank.Entities;
using Homebank.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homebank.Web.Models;

namespace Homebank.Web.Controllers
{
	public class HomeController : BaseController
	{
		private UserRepository _userRepository;
		private readonly AccountRepository _accountRepository;

		public HomeController(UserRepository userRepository, AccountRepository accountRepository, TemplateRepository templateRepository)
			: base(userRepository, templateRepository, accountRepository)
		{
			_userRepository = userRepository;
			_accountRepository = accountRepository;
		}

		public ActionResult Index()
		{
			return View();
		}

		[Authorize]
		public ActionResult Overview()
		{
			var accounts = _accountRepository.GetAllByUser(HomebankUser, true);

			var model = new AccountOverviewModel();
			model.Accounts = new List<AccountModel>();

			foreach (var account in accounts)
			{
				var a = new AccountModel
				{
					Id = account.Id,
					Name = account.Name,
					FutureBalance = _accountRepository.GetFutureBalance(account.Id),
					CurrentBalance = _accountRepository.GetCurrentBalance(account.Id)
				};

				model.Accounts.Add(a);
			}

			ViewBag.Header = "Overview";
			return View(model);
		}
	}
}