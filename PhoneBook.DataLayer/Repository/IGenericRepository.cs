using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where = null);
        Task<TEntity> GetById(object id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateData(TEntity entity);
        Task<bool> Delete(object id);
    }
}
