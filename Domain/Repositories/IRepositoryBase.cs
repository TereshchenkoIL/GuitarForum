using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression,
           CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync( CancellationToken cancellationToken = default);
        void Create(T entity);
        void Update(T entity); 
        void Delete(T entity);
        
    }
}