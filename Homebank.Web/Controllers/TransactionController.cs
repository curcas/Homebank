using Homebank.Entities;
using Homebank.Repositories;
using Homebank.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class TransactionController : BaseController
    {
        private readonly AccountRepository _accountRepository;
	    private readonly CategoryRepository _categoryRepository;
		private readonly TransactionRepository _transactionRepository;
	    private readonly TemplateRepository _templateRepository;
		private readonly BookingRepository _bookingRepository;

		public TransactionController(UserRepository userRepository, AccountRepository accountRepository, TransactionRepository transactionRepository, CategoryRepository categoryRepository, TemplateRepository templateRepository, BookingRepository bookingRepository)
		    : base(userRepository, templateRepository, accountRepository)
	    {
		    _accountRepository = accountRepository;
			_categoryRepository = categoryRepository;
		    _transactionRepository = transactionRepository;
			_templateRepository = templateRepository;
			_bookingRepository = bookingRepository;
	    }

	    public ActionResult Add(int id, int template = 0)
	    {
		    var model = new TransactionModel();
		    var account = _accountRepository.GetById(HomebankUser, id);

		    if (account != null)
		    {
			    model.Account = account.Name;
			    model.AccountId = account.Id;
			    model.Date = DateTime.Now;

			    if (template != 0)
			    {
				    var tmpl = _templateRepository.GetById(HomebankUser, template);

				    if (tmpl != null && account.Id == tmpl.Account.Id)
				    {
					    model.CategoryId = tmpl.Category.Id;
					    model.Amount = tmpl.Amount;
					    model.Description = tmpl.Description;

					    if (tmpl.ReferenceAccount != null)
					    {
						    model.ReferenceAccountId = tmpl.ReferenceAccount.Id;
					    }
				    }
			    }

				PrepareCategories(model);
				PrepareReferenceAccounts(model);
		    }
		    else
		    {
			    ViewData["Error"] = "The account was not found!";
			}

			ViewBag.Header = "Add transaction";
		    return View("Edit", model);
	    }

		[ValidateAntiForgeryToken]
		[HttpPost]
	    public ActionResult Add(int id, TransactionModel model)
	    {
			if (ModelState.IsValid)
			{
				var transaction = new Transaction
				{
					Category = _categoryRepository.GetById(HomebankUser, model.CategoryId),
					Date = model.Date,
					Description = model.Description
				};

				var booking = new Booking
				{
					Account = _accountRepository.GetById(HomebankUser, model.AccountId),
					Amount = model.Amount,
					Transaction = transaction
				};

				if (model.ReferenceAccountId != null)
				{
					var referenceBooking = new Booking
					{
						Account = _accountRepository.GetById(HomebankUser, model.ReferenceAccountId.Value),
						Amount = model.Amount*-1,
						Transaction = transaction
					};

					transaction.Bookings.Add(referenceBooking);
				}

				transaction.Bookings.Add(booking);

				_transactionRepository.Save(transaction);
				_transactionRepository.SaveChanges();

				return RedirectToAction("Show", "Account", new {id, page = 1});
			}

			PrepareCategories(model);
			PrepareReferenceAccounts(model);

			ViewBag.Header = "Add transaction";
			return View("Edit", model);
	    }

	    public ActionResult Edit(int id, int account)
	    {
		    var trans = _transactionRepository.GetById(HomebankUser, id);
		    var a = _accountRepository.GetById(HomebankUser, account);
		    var model = new TransactionModel();

		    if (trans != null && account > 0)
		    {
			    model.DataId = trans.Id;
			    model.Account = a.Name;
			    model.AccountId = a.Id;
			    model.Amount = trans.Bookings.First(p => p.Account.Id == a.Id).Amount;
			    model.CategoryId = trans.Category.Id;
			    model.Date = trans.Date;
			    model.Description = trans.Description;

			    if (trans.Bookings.Count == 2)
			    {
				    model.ReferenceAccountId = trans.Bookings.First(p => p.Account.Id != a.Id).Account.Id;
			    }

				PrepareCategories(model);
				PrepareReferenceAccounts(model);
		    }

		    ViewBag.Error = "Account or transaction not found!";

			ViewBag.Header = "Edit transaction";
		    return View(model);
	    }

		[ValidateAntiForgeryToken]
		[HttpPost]
	    public ActionResult Edit(int id, int account, TransactionModel model)
	    {
			var trans = _transactionRepository.GetById(HomebankUser, id);
			var a = _accountRepository.GetById(HomebankUser, account);

		    if (ModelState.IsValid)
		    {
			    trans.Category = _categoryRepository.GetById(HomebankUser, model.CategoryId);
			    trans.Date = model.Date;
			    trans.Description = model.Description;

				foreach (var b in trans.Bookings.ToList())
			    {
				    _bookingRepository.Remove(b);
			    }

				var booking = new Booking
				{
					Account = a,
					Amount = model.Amount,
					Transaction = trans
				};

				if (model.ReferenceAccountId != null)
				{
					var referenceBooking = new Booking
					{
						Account = _accountRepository.GetById(HomebankUser, model.ReferenceAccountId.Value),
						Amount = model.Amount * -1,
						Transaction = trans
					};

					trans.Bookings.Add(referenceBooking);
				}

				trans.Bookings.Add(booking);

				_transactionRepository.Save(trans);
				_transactionRepository.SaveChanges();

			    return RedirectToAction("Show", "Account", new {id = account});
		    }

			ViewBag.Header = "Edit transaction";
		    return View(model);
	    }

	    public ActionResult Remove(int id, int account)
	    {
		    var transaction = _transactionRepository.GetById(HomebankUser, id);

		    if (transaction != null)
		    {
			    foreach (var booking in transaction.Bookings.ToList())
			    {
				    _bookingRepository.Remove(booking);
			    }

			    _transactionRepository.Remove(transaction);
				_transactionRepository.SaveChanges();

			    return RedirectToAction("Show", "Account", new {id = account});
		    }

		    return HttpNotFound();
	    }

	    [NonAction]
	    private void PrepareCategories(TransactionModel model)
	    {
		    foreach (var category in _categoryRepository.GetAllByUser(HomebankUser, true))
		    {
				model.Categories.Add(category.Id, category.Name);
		    }
	    }

	    [NonAction]
	    private void PrepareReferenceAccounts(TransactionModel model)
	    {
		    foreach (var account in _accountRepository.GetAllByUser(HomebankUser, true))
		    {
			    if (account.Id != model.AccountId)
			    {
				    model.ReferenceAccounts.Add(account.Id, account.Name);
			    }
		    }
	    }
	}
}