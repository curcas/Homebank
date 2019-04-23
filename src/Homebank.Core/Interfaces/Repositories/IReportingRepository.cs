using Homebank.Core.Entities;
using Homebank.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface IReportingRepository : IBaseRepository<object>
    {
        IEnumerable<ReportingRecord> SearchTransactions(User user, IList<int> categories, IList<int> accounts, DateTime from, DateTime until, bool includeTransactionsToOtherAccounts, ReportingType reportingType);
    }
}
