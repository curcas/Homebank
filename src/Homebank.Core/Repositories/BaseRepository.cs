using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Homebank.Core.Repositories
{
	public class BaseRepository<T> where T : class
	{
		protected DatabaseContext _databaseContext;
		private readonly DbSet<T> _set;

		public BaseRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
			_set = databaseContext.Set<T>();
		}

		public void Save(T entity)
		{
			_set.AddOrUpdate(entity);
		}

		public void Remove(T entity)
		{
			_set.Remove(entity);
		}

		public void SaveChanges()
		{
			_databaseContext.SaveChanges();
		}
	}
}
