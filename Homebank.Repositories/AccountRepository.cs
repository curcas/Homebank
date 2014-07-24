using Homebank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homebank.Repositories
{
	public class AccountRepository : BaseRepository<Account>
	{
		public AccountRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Account GetById(User user, int accountId)
		{
			return _databaseContext.Accounts.FirstOrDefault(a => a.User.Id == user.Id && a.Id == accountId);
		}

		public Account GetByName(User user, string name)
		{
			return _databaseContext.Accounts.FirstOrDefault(a => a.User.Id == user.Id && a.Name == name);
		}

		public IList<Account> GetAllByUser(User user, bool onlyActiveAccounts)
		{
			var query = _databaseContext.Accounts.Where(a => a.User.Id == user.Id).OrderBy(a => a.Name).AsQueryable();

			if (onlyActiveAccounts)
			{
				query = query.Where(p => p.Active);
			}

			return query.ToList();
		}

		public bool IsNameUnique(int userId, int accountId, string name)
		{
			return _databaseContext.Accounts.FirstOrDefault(p => p.Name == name && p.User.Id == userId && p.Id != accountId) == null;
		}

		public decimal GetCurrentBalance(int account)
		{
			return _databaseContext.Bookings.Where(p => p.Account.Id == account && p.Transaction.Date <= DateTime.Now).Select(p => p.Amount).DefaultIfEmpty(0).Sum(p => p);
		}

		public decimal GetFutureBalance(int account)
		{
			return _databaseContext.Bookings.Where(p => p.Account.Id == account).Select(p => p.Amount).DefaultIfEmpty(0).Sum(p => p);
		}
	}
}
