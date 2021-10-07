using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(DataContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Topic>> GetByConditionAsync(Expression<Func<Topic, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .Where(expression)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken: cancellationToken)
                : await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .Where(expression)
                    .ToListAsync(cancellationToken: cancellationToken);
        }

        public override async Task<IEnumerable<Topic>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken: cancellationToken)
                : await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Topic>> GetAllByCreatorIdAsync(string creatorId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return await GetByConditionAsync(x => x.Creator.Id == creatorId, trackChanges, cancellationToken);
        }

        public async Task<IEnumerable<Topic>> GetAllByCategoryIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return await GetByConditionAsync(x => x.Category.Id== categoryId, trackChanges, cancellationToken);
        }

        public async Task<Topic> GetByIdAsync(Guid topicId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == topicId, cancellationToken: cancellationToken)
                : await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .Include(x => x.Likes)
                    .FirstOrDefaultAsync(x => x.Id == topicId, cancellationToken: cancellationToken);
        }
    }
}