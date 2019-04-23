using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class AccountController : BaseController
    {
	    private readonly IAccountRepository _accountRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly ITemplateRepository _templateRepository;

		public AccountController(IUserRepository userRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, ITemplateRepository templateRepository)
		    : base(userRepository)
	    {
		    _accountRepository = accountRepository;
		    _transactionRepository = transactionRepository;
			_templateRepository = templateRepository;
	    }

		public ActionResult Show(int id, int page = 1)
		{
			var account = _accountRepository.GetById(HomebankUser, id);
            var templates = _templateRepository.GetAllByAccount(account.Id);

			if (account == null)
			{
                return NotFound("Account not found!");
			}

			if (page < 1)
			{
				page = 1;
			}

            var model = new AccountShowModel
            {
                Account = account,
                CurrentBalance = _accountRepository.GetCurrentBalance(id),
                FutureBalance = _accountRepository.GetFutureBalance(id),
                Transactions = _transactionRepository.GetByAccount(id, page, out int totalTransactions),
                TotalTransactions = totalTransactions,
                Templates = templates,
                CurrentPage = page
            };

            return View(model);
		}

	    public ActionResult Add()
	    {
		    return View("Edit", new AccountModel {Active = true});
	    }

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Add(AccountModel model)
		{
			if (ModelState.IsValid)
			{
				var account = new Account {Name = model.Name, User = HomebankUser, Active = model.Active, ControlDate = model.ControlDate};

				_accountRepository.Add(account);
				_accountRepository.SaveChanges();

				return RedirectToAction("List", "Account");
			}

			return View("Edit", model);
		}

		public ActionResult List()
		{
			return View(_accountRepository.GetAllByUser(HomebankUser, false));
		}

		public ActionResult Edit(int id)
		{
			var account = _accountRepository.GetById(HomebankUser, id);

			if (account == null)
			{
                return NotFound("Account not found!");
			}

			var model = new AccountModel
			{
				Id = account.Id,
				Name = account.Name,
				Active = account.Active,
                ControlDate = account.ControlDate
			};

			return View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(int id, AccountModel model)
		{
			if (ModelState.IsValid)
			{
				var account = _accountRepository.GetById(HomebankUser, id);

				if (account == null)
				{
                    return NotFound("Account not found!");
				}

				account.Name = model.Name;
				account.Active = model.Active;
                account.ControlDate = model.ControlDate;

				_accountRepository.Update(account);
				_accountRepository.SaveChanges();

                return RedirectToAction("List", "Account");
			}

			return View(model);
		}

		public ActionResult Remove(int id)
		{
			var account = _accountRepository.GetById(HomebankUser, id);

			if (account != null)
			{
				foreach (var template in _templateRepository.GetAllByAccountForDeletion(account.Id))
				{
					_templateRepository.Remove(template);
				}

				_accountRepository.Remove(account);
				_accountRepository.SaveChanges();

				return RedirectToAction("List", "Account");
			}

            return NotFound("Account not found");
		}
	}
}