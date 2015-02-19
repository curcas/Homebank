using System;
using System.Collections.Generic;
using System.Linq;

namespace Homebank.Repositories
{
	public class ReportingRepository : BaseRepository<object>
	{
		public ReportingRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IEnumerable<ReportingRecord> SearchTransactions(IEnumerable<int> categories, IEnumerable<int> accounts, DateTime from, DateTime until, ReportingType reportingType)
		{
			return _databaseContext.Bookings.Where(p =>
				accounts.Contains(p.Account.Id)
				&& categories.Contains(p.Transaction.Category.Id)
				&& reportingType == ReportingType.Earnings ? p.Amount > 0 : p.Amount < 0
				&& p.Transaction.Date >= from
				&& p.Transaction.Date >= until)
				.Select(p => new ReportingRecord
				{
					Id = p.Id,
					Account = p.Account.Id,
					Category = p.Transaction.Category.Id,
					Date = p.Transaction.Date,
					Description = p.Transaction.Description,
					Amount = p.Amount
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
		public int Category { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
