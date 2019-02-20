using System.Linq;
using System.Web;
using Homebank.Core.Entities;
using Homebank.Core.Repositories;
using Homebank.Web.Models;
using System.Web.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class TemplateController : BaseController
	{
		private readonly TemplateRepository _templateRepository;
		private readonly AccountRepository _accountRepository;
		private readonly CategoryRepository _categoryRepository;

		public TemplateController(UserRepository userRepository, TemplateRepository templateRepository, AccountRepository accountRepository, CategoryRepository categoryRepository)
			: base(userRepository, templateRepository, accountRepository)
		{
			_templateRepository = templateRepository;
			_accountRepository = accountRepository;
			_categoryRepository = categoryRepository;
		}

		public ActionResult List()
		{
			return View(_templateRepository.GetAllByUser(HomebankUser));
		}

		public ActionResult Add()
		{
			var model = new TemplateModel();

			model.ReferenceAccounts.Add(0, "No reference account");
			foreach (var account in _accountRepository.GetAllByUser(HomebankUser, true))
			{
				model.Accounts.Add(account.Id, account.Name);
				model.ReferenceAccounts.Add(account.Id, account.Name);
			}

			foreach (var category in _categoryRepository.GetAllByUser(HomebankUser, true))
			{
				model.Categories.Add(category.Id, category.Name);
			}

			ViewBag.Header = "Add template";
			return View("Edit", model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Add(TemplateModel model)
		{
			if (ModelState.IsValid)
			{
				var template = new Template
				{
					Name = model.Name,
					Amount = model.Amount,
					Description = model.Description,
					Category = _categoryRepository.GetById(HomebankUser, model.CategoryId),
					Account = _accountRepository.GetById(HomebankUser, model.AccountId),
					ReferenceAccount = _accountRepository.GetById(HomebankUser, model.ReferenceAccountId),
					User = HomebankUser
				};

				_templateRepository.Save(template);
				_templateRepository.SaveChanges();

				return RedirectToAction("List", "Template");
			}

			model.ReferenceAccounts.Add(0, "No reference account");
			foreach (var account in _accountRepository.GetAllByUser(HomebankUser, true))
			{
				model.Accounts.Add(account.Id, account.Name);
				model.ReferenceAccounts.Add(account.Id, account.Name);
			}

			foreach (var category in _categoryRepository.GetAllByUser(HomebankUser, true))
			{
				model.Categories.Add(category.Id, category.Name);
			}

			ViewBag.Header = "Add template";
			return View("Edit", model);
		}

		public ActionResult Edit(int id)
		{
			var template = _templateRepository.GetById(HomebankUser, id);

			if (template == null)
			{
				throw new HttpException(404, "Template not found!");
			}

			var model = new TemplateModel
			{
				Id = template.Id,
				Name = template.Name,
				Amount = template.Amount,
				Description = template.Description,
				CategoryId = template.Category.Id,
				AccountId = template.Account.Id,
				ReferenceAccountId = template.ReferenceAccount != null ? template.ReferenceAccount.Id : 0
			};

			model.ReferenceAccounts.Add(0, "No reference account");
			foreach (var account in _accountRepository.GetAllByUser(HomebankUser, true))
			{
				model.Accounts.Add(account.Id, account.Name);
				model.ReferenceAccounts.Add(account.Id, account.Name);
			}

			foreach (var category in _categoryRepository.GetAllByUser(HomebankUser, true))
			{
				model.Categories.Add(category.Id, category.Name);
			}

			if (!model.Categories.ContainsKey(template.Category.Id))
			{
				model.Categories.Add(template.Category.Id, template.Category.Name);
			}

			ViewBag.Header = "Edit template";
			return View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(int id, TemplateModel model)
		{
			var template = _templateRepository.GetById(HomebankUser, id);

			if (ModelState.IsValid)
			{
				if (template == null)
				{
					throw new HttpException(404, "Template not found");
				}

				template.Name = model.Name;
				template.Amount = model.Amount;
				template.Description = model.Description;
				template.Category = _categoryRepository.GetById(HomebankUser, model.CategoryId);
				template.Account = _accountRepository.GetById(HomebankUser, model.AccountId);
				template.ReferenceAccount = _accountRepository.GetById(HomebankUser, model.ReferenceAccountId);

				_templateRepository.Save(template);
				_templateRepository.SaveChanges();

				ViewBag.Success = "Updated template";
			}

			model.ReferenceAccounts.Add(0, "No reference account");
			foreach (var account in _accountRepository.GetAllByUser(HomebankUser, true))
			{
				model.Accounts.Add(account.Id, account.Name);
				model.ReferenceAccounts.Add(account.Id, account.Name);
			}

			foreach (var category in _categoryRepository.GetAllByUser(HomebankUser, true))
			{
				model.Categories.Add(category.Id, category.Name);
			}

			if (!model.Categories.ContainsKey(template.Category.Id))
			{
				model.Categories.Add(template.Category.Id, template.Category.Name);
			}

			ViewBag.Header = "Edit template";
			return View(model);
		}

		public ActionResult Remove(int id)
		{
			var template = _templateRepository.GetById(HomebankUser, id);

			if (template == null)
			{
				throw new HttpException(404, "Template not found!");
			}

			_templateRepository.Remove(template);
			_templateRepository.SaveChanges();

			return RedirectToAction("List", "Template");
		}
	}
}