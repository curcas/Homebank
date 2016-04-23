using System.Data.Entity;
using System.Security.Cryptography;
using Homebank.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Homebank.Repositories
{
	public class TransactionRepository : BaseRepository<Transaction>
	{
		public TransactionRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IList<Transaction> GetByAccount(int accountId, int page, out int totalTransactions)
		{
			var baseQuery = _databaseContext.Transactions
				.Include(p => p.Bookings)
				.Include(p => p.Category)
				.Where(p => p.Bookings.Any(x => x.Account.Id == accountId));

			totalTransactions = baseQuery.Count();

			var query = baseQuery
				.OrderByDescending(p => p.Date)
				.ThenByDescending(p => p.Id)
				.Skip(100 * (page - 1))
				.Take(100);

			return query.ToList();
		}

		public Transaction GetById(User user, int id)
		{
			return
				_databaseContext.Transactions.FirstOrDefault(p => p.Id == id && p.Bookings.Any(b => b.Account.User.Id == user.Id));
		}

		public IList<Transaction> GetAll()
		{
			return _databaseContext.Transactions.ToList();
		}

		public IList<string> GetDescriptionsList(User user, string description)
		{
			return _databaseContext.Transactions
				.Where(p => p.Bookings.Any(b => b.Account.User.Id == user.Id))
				.Where(p => p.Description.Contains(description))
				.Select(p => p.Description)
				.Distinct()
				.ToList();
		} 
	}
}
