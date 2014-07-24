using Homebank.Entities;
using Homebank.Repositories;
using Homebank.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class AccountController : BaseController
    {
	    private readonly AccountRepository _accountRepository;
		private readonly TransactionRepository _transactionRepository;
		private readonly TemplateRepository _templateRepository;

		public AccountController(UserRepository userRepository, AccountRepository accountRepository, TransactionRepository transactionRepository, TemplateRepository templateRepository)
		    : base(userRepository, templateRepository, accountRepository)
	    {
		    _accountRepository = accountRepository;
		    _transactionRepository = transactionRepository;
			_templateRepository = templateRepository;
	    }

		public ActionResult Show(int id, int page = 1)
		{
			var account = _accountRepository.GetById(HomebankUser, id);

			if (account == null)
			{
				throw new HttpException(404, "Account not found!");
			}

			if (page < 1)
			{
				page = 1;
			}

			int totalTransactions;
			var model = new AccountShowModel
			{
				Account = account,
				CurrentBalance = _accountRepository.GetCurrentBalance(id),
				FutureBalance = _accountRepository.GetFutureBalance(id),
				Transactions = _transactionRepository.GetByAccount(id, page, out totalTransactions),
				TotalTransactions = totalTransactions,
				CurrentPage = page
			};

			return View(model);
		}

	    public ActionResult Add()
	    {
		    ViewBag.Header = "Add account";
		    return View("Edit", new AccountModel {Active = true});
	    }

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Add(AccountModel model)
		{
			if (ModelState.IsValid)
			{
				var account = new Account {Name = model.Name, User = HomebankUser, Active = model.Active};

				_accountRepository.Save(account);
				_accountRepository.SaveChanges();

				return RedirectToAction("List", "Account");
			}

			ViewBag.Header = "Add account";
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
				throw new HttpException(404, "Account not found!");
			}

			var model = new AccountModel
			{
				Id = account.Id,
				Name = account.Name,
				Active = account.Active
			};

			ViewBag.Header = "Edit account";
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
					throw new HttpException(404, "Account not found!");
				}

				account.Name = model.Name;
				account.Active = model.Active;

				_accountRepository.Save(account);
				_accountRepository.SaveChanges();

				ViewBag.Success = "Updated account.";
			}

			ViewBag.Header = "Edit account";
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

			throw new HttpException(404, "Account not found");
		}
	}
}