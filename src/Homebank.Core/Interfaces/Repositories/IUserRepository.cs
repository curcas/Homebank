using Homebank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Get(string name);
        User Get(int id);
    }
}
