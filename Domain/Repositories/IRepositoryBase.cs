using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression,
            bool trackChanges, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);
        Task Create(T entity);
        Task Update(T entity); 
        Task Delete(T entity);
        
    }
}