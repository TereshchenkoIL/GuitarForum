using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<Category> GetByIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken: cancellationToken)
                : await Context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId,
                    cancellationToken: cancellationToken);

        }
    }
}