using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Repository
{
    public interface IRepository<T> where T :class
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAllById(int id);

        Task<int> Add(T entity);

        Task<int> Delete(int id);

        Task<int> Update(T entity);
    }
}
