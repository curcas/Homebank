using Homebank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Account GetById(User user, int accountId);
        Account GetByName(User user, string name);
        IList<Account> GetAllByUser(User user, bool onlyActiveAccounts);
        bool IsNameUnique(int userId, int accountId, string name);
        decimal GetCurrentBalance(int account);
        decimal GetFutureBalance(int account);
    }
}
