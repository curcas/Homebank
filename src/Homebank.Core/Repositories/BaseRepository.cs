using Microsoft.EntityFrameworkCore;

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

		public void Add(T entity)
		{
			_set.Add(entity);
		}

        public void Update(T entity)
        {
            _set.Update(entity);
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
