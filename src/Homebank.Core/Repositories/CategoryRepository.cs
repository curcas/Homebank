using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Homebank.Core.Repositories
{
	public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
		public CategoryRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Category GetById(User user, int id)
		{
			return _databaseContext.Categories.FirstOrDefault(p => p.Id == id && p.User.Id == user.Id);
		}

		public Category GetByName(User user, string name)
		{
			return _databaseContext.Categories.FirstOrDefault(p => p.User.Id == user.Id && p.Name == name);
		}

		public IList<Category> GetAllByUser(User user, bool onlyActiveCategories)
		{
			var query = _databaseContext.Categories.Where(p => p.User.Id == user.Id).OrderBy(p => p.Name).AsQueryable();

			if (onlyActiveCategories)
			{
				query = query.Where(p => p.Active);
			}

			return query.ToList();
		}

		public bool IsNameUnique(int userId, int categoryId, string name)
		{
			return _databaseContext.Categories.FirstOrDefault(p => p.Name == name && p.User.Id == userId && p.Id != categoryId) == null;
		}
	}
}
