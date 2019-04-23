using System;
using System.Collections.Generic;
using System.Linq;
using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;

namespace Homebank.Core.Repositories
{
	public class ReportingRepository : BaseRepository<object>, IReportingRepository
    {
		public ReportingRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IEnumerable<ReportingRecord> SearchTransactions(User user, IList<int> categories, IList<int> accounts, DateTime from, DateTime until, bool includeTransactionsToOtherAccounts, ReportingType reportingType)
		{
			var query = _databaseContext.Bookings.Where(p =>
				p.Transaction.Date >= from
				&& p.Transaction.Date <= until
				&& p.Account.User.Id == user.Id);

			if (!includeTransactionsToOtherAccounts)
			{
				query = query.Where(p => p.Transaction.Bookings.Count == 1);
			}

			if (!categories.Contains(0))
			{
				query = query.Where(p => categories.Contains(p.Transaction.Category.Id));
			}

			if (!accounts.Contains(0))
			{
				query = query.Where(p => accounts.Contains(p.Account.Id));
			}

			query = reportingType == ReportingType.Earnings ? query.Where(p => p.Amount > 0) : query.Where(p => p.Amount < 0);

			return query.Select(p => new ReportingRecord
			{
				Id = p.Transaction.Id,
				Account = p.Account.Id,
				AccountName = p.Account.Name,
				Category = p.Transaction.Category.Id,
				CategoryName = p.Transaction.Category.Name,
				Date = p.Transaction.Date,
				Description = p.Transaction.Description,
				Amount = reportingType == ReportingType.Earnings ? p.Amount : p.Amount * -1
			})
				.ToList();
		}
	}

	public enum ReportingType
	{
		Earnings,
		Expenses
	}

	public class ReportingRecord
	{
		public int Id { get; set; }
		public int Account { get; set; }
		public string AccountName { get; set; }
		public int Category { get; set; }
		public string CategoryName { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
