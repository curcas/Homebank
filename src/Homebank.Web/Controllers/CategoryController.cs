using System.IO;
using System.Linq;
using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class CategoryController : BaseController
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(IUserRepository userRepository, ICategoryRepository categoryRepository)
			: base(userRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public ActionResult List()
		{
			return View(_categoryRepository.GetAllByUser(HomebankUser, false));
		}

		public ActionResult Add()
		{
			return View("Edit", new CategoryModel {Active = true});
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Add(CategoryModel model)
		{
			if (ModelState.IsValid)
			{
				var category = new Category {Name = model.Name, User = HomebankUser, Active = model.Active};

				_categoryRepository.Add(category);
				_categoryRepository.SaveChanges();

				return RedirectToAction("List", "Category");
			}

			return View("Edit", model);
		}

		public ActionResult Edit(int id)
		{
			var category = _categoryRepository.GetById(HomebankUser, id);

			if (category == null)
			{
                return NotFound("Category not found!");
			}

			var model = new CategoryModel
			{
				Id = category.Id,
				Name = category.Name,
				Active =  category.Active
			};

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
                    return NotFound("Category not found!");
				}

				category.Name = model.Name;
				category.Active = model.Active;

				_categoryRepository.Update(category);
				_categoryRepository.SaveChanges();

                return RedirectToAction("List", "Category");
            }

			return View(model);
		}

		public ActionResult Remove(int id)
		{
			var category = _categoryRepository.GetById(HomebankUser, id);

			if (category == null)
			{
                return NotFound("Category not found!");
			}

			if (category.Transactions.Any())
			{
                return Unauthorized("You're not allowed to delete this category!");
			}

			_categoryRepository.Remove(category);
			_categoryRepository.SaveChanges();

			return RedirectToAction("List", "Category");
		}
	}
}