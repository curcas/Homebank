using Homebank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface ITemplateRepository : IBaseRepository<Template>
    {
        Template GetById(User user, int id);
        IList<Template> GetAllByUser(User user);
        IList<Template> GetAllByAccount(int accountId);
        IList<Template> GetAllByAccountForDeletion(int accountId);
        bool IsNameUnique(int userId, int templateId, string name, int accountId);
    }
}
