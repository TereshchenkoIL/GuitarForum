using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(DataContext context) : base(context)
        {
        }

        public async Task<Like> GetLike(string appUserId, Guid topicId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Likes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.AppUserId == appUserId && x.TopicId == topicId, cancellationToken: cancellationToken)
                : await Context.Likes
                    .FirstOrDefaultAsync(x => x.AppUserId == appUserId && x.TopicId == topicId, cancellationToken: cancellationToken);

        }
    }
}