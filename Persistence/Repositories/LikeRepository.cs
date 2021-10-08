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

        public async Task<Like> GetLike(string appUserId, Guid topicId, CancellationToken cancellationToken = default)
        {
            return await Context.Likes
                    .FirstOrDefaultAsync(x => x.AppUserId == appUserId && x.TopicId == topicId, cancellationToken: cancellationToken);

        }
    }
}