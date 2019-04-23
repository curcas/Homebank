using System;
using System.Collections.Generic;
using System.Text;

namespace Homebank.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
