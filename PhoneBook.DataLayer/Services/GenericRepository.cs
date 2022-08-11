using Microsoft.EntityFrameworkCore;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Set TEntity
        protected readonly ApplicationDbContext _context;
        protected DbSet<TEntity> _dbset;
        public GenericRepository(ApplicationDbContext Context)
        {
            _context = Context;
            _dbset = Context.Set<TEntity>();
        }
        #endregion

        #region Methods
        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbset;
            if (where != null)
            {
                query = query.Where(where);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            return await _dbset.FindAsync(id);
        }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(object id)
        {
            var entity = await _dbset.FindAsync(id);

            _dbset.Remove(entity);
            return true;
        }

        public virtual async Task<bool> UpdateData(TEntity entity)
        {
            _dbset.Update(entity);
            return true;
        }
        #endregion
    }
}
