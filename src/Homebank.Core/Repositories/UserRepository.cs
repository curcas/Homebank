using Homebank.Core.Entities;
using System.Linq;

namespace Homebank.Core.Repositories
{
	public class UserRepository : BaseRepository<User>
	{
		public UserRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public User Get(string name)
		{
			return _databaseContext.Users.FirstOrDefault(p => p.Name == name);
		}

		public User Get(int id)
		{
			return _databaseContext.Users.FirstOrDefault(p => p.Id == id);
		}
	}
}
