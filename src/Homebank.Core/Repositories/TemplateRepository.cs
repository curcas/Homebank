using Homebank.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Homebank.Core.Repositories
{
	public class TemplateRepository : BaseRepository<Template>
	{
		public TemplateRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Template GetById(User user, int id)
		{
			return _databaseContext.Templates.FirstOrDefault(p => p.User.Id == user.Id && p.Id == id);
		}

		public IList<Template> GetAllByUser(User user)
		{
			return _databaseContext.Templates.Where(p => p.User.Id == user.Id).OrderBy(p => p.Account.Name).ThenBy(p => p.Name).ToList();
		}

		public IList<Template> GetAllByAccount(int accountId)
		{
			return _databaseContext.Templates.Where(p => p.Account.Id == accountId).OrderBy(p => p.Name).ToList();
		}

		public IList<Template> GetAllByAccountForDeletion(int accountId)
		{
			return _databaseContext.Templates.Where(p => p.Account.Id == accountId || p.ReferenceAccount.Id == accountId).ToList();
		} 

		public bool IsNameUnique(int userId, int templateId, string name, int accountId)
		{
			return _databaseContext.Templates.FirstOrDefault(p => p.Name == name && p.Account.Id == accountId &&  p.User.Id == userId && p.Id != templateId) == null;
		}
	}
}
