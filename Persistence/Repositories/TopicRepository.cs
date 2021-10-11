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

        public override async Task<IEnumerable<Topic>> GetByConditionAsync(Expression<Func<Topic, bool>> expression,  CancellationToken cancellationToken = default)
        {
            return await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Topics)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Photo)
                    .Include(x => x.Likes)
                    .Where(expression)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync(cancellationToken: cancellationToken);
        }

        public override async Task<IEnumerable<Topic>> GetAllAsync( CancellationToken cancellationToken = default)
        {
            return  await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Topics)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Photo)
                    .Include(x => x.Likes)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Topic>> GetAllByCreatorIdAsync(string creatorId,  CancellationToken cancellationToken = default)
        {
            return await GetByConditionAsync(x => x.Creator.Id == creatorId,  cancellationToken);
        }

        public async Task<IEnumerable<Topic>> GetAllByCategoryIdAsync(Guid categoryId,  CancellationToken cancellationToken = default)
        {
            return await GetByConditionAsync(x => x.Category.Id== categoryId,  cancellationToken);
        }

        public async Task<Topic> GetByIdAsync(Guid topicId,  CancellationToken cancellationToken = default)
        {
            return  await Context.Topics
                    .Include(x => x.Category)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Topics)
                    .Include(x => x.Creator)
                    .ThenInclude(x => x.Photo)
                    .Include(x => x.Likes)
                    .FirstOrDefaultAsync(x => x.Id == topicId, cancellationToken: cancellationToken);
        }
    }
}