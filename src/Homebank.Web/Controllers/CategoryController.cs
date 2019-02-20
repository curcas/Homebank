using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homebank.Entities;
using Homebank.Repositories;
using Homebank.Web.Models;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class CategoryController : BaseController
	{
		private readonly CategoryRepository _categoryRepository;

		public CategoryController(UserRepository userRepository, CategoryRepository categoryRepository, TemplateRepository templateRepository, AccountRepository accountRepository)
			: base(userRepository, templateRepository, accountRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public ActionResult List()
		{
			return View(_categoryRepository.GetAllByUser(HomebankUser, false));
		}

		public ActionResult Add()
		{
			ViewBag.Header = "Add category";
			return View("Edit", new CategoryModel {Active = true});
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Add(CategoryModel model)
		{
			if (ModelState.IsValid)
			{
				var category = new Category {Name = model.Name, User = HomebankUser, Active = model.Active};

				_categoryRepository.Save(category);
				_categoryRepository.SaveChanges();

				return RedirectToAction("List", "Category");
			}

			ViewBag.Header = "Add category";
			return View("Edit", model);
		}

		public ActionResult Edit(int id)
		{
			var category = _categoryRepository.GetById(HomebankUser, id);

			if (category == null)
			{
				throw new HttpException(404, "Category not found!");
			}

			var model = new CategoryModel
			{
				Id = category.Id,
				Name = category.Name,
				Active =  category.Active
			};

			ViewBag.Header = "Edit category";
			return View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(int id, CategoryModel model)
		{
			if (ModelState.IsValid)
			{
				var category = _categoryRepository.GetById(HomebankUser, id);

				if (category == null)
				{
					throw new HttpException(404, "Category not found!");
				}

				category.Name = model.Name;
				category.Active = model.Active;

				_categoryRepository.Save(category);
				_categoryRepository.SaveChanges();

				ViewBag.Success = "Updated category";
			}

			ViewBag.Header = "Edit category";
			return View(model);
		}

		public ActionResult Remove(int id)
		{
			var category = _categoryRepository.GetById(HomebankUser, id);

			if (category == null)
			{
				throw new HttpException(404, "Category not found!");
			}

			if (category.Transactions.Any())
			{
				throw new HttpException(400, "You're not allowed to delete this category!");
			}

			_categoryRepository.Remove(category);
			_categoryRepository.SaveChanges();

			return RedirectToAction("List", "Category");
		}
	}
}