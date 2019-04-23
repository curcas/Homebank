using Homebank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        IList<Transaction> GetByAccount(int accountId, int page, out int totalTransactions);
        Transaction GetById(User user, int id);
        IList<Transaction> GetAll();
        IList<string> GetDescriptionsList(User user, string description);
    }
}
