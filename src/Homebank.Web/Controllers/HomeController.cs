using Homebank.Core.Repositories;
using System.Collections.Generic;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Homebank.Core.Interfaces.Repositories;

namespace Homebank.Web.Controllers
{
	public class HomeController : BaseController
	{
		private IUserRepository _userRepository;
		private readonly IAccountRepository _accountRepository;

		public HomeController(IUserRepository userRepository, IAccountRepository accountRepository)
			: base(userRepository)
		{
			_userRepository = userRepository;
			_accountRepository = accountRepository;
		}

		[Authorize]
		public ActionResult Index()
		{
			var accounts = _accountRepository.GetAllByUser(HomebankUser, true);

            var model = new AccountOverviewModel()
            {
                Accounts = new List<AccountModel>()
            };

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

			return View(model);
		}
	}
}