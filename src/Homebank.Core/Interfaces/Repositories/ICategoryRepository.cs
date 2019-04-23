using Homebank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Category GetById(User user, int id);
        Category GetByName(User user, string name);
        IList<Category> GetAllByUser(User user, bool onlyActiveCategories);
        bool IsNameUnique(int userId, int categoryId, string name);
    }
}
