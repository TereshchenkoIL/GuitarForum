using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }

        public async Task<Comment> GetByIdAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            return await Context.Comments
                    .Include(x => x.Author)
                    .ThenInclude(p => p.Photo)
                    .Include(t => t.Topic)
                    .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetAllByTopicAsync(Guid topicId,
            CancellationToken cancellationToken = default)
        {
            return  await Context.Comments
                    .Include(x => x.Author)
                    .ThenInclude(p => p.Photo)
                    .Include(t => t.Topic)
                    .Where(x => x.Topic.Id == topicId)
                    .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}