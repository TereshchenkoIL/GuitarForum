using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext Context;

        protected RepositoryBase(DataContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression,  CancellationToken cancellationToken = default)
        {
            return  await Context.Set<T>()
                    .Where(expression).ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync( CancellationToken cancellationToken = default)
        {
            return await Context.Set<T>()
                    .ToListAsync(cancellationToken: cancellationToken);

        }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}