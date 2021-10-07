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

        public async Task<Comment> GetByIdAsync(Guid commentId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Comments
                    .Include(x => x.Author)
                    .Include(t => t.Topic)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken: cancellationToken)
                : await Context.Comments
                    .Include(x => x.Author)
                    .Include(t => t.Topic)
                    .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetAllByTopicAsync(Guid topicId, bool trackChanges,
            CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Comments
                    .Include(x => x.Author)
                    .Include(t => t.Topic)
                    .AsNoTracking()
                    .Where(x => x.Topic.Id == topicId)
                    .ToListAsync(cancellationToken: cancellationToken)
                : await Context.Comments
                    .Include(x => x.Author)
                    .Include(t => t.Topic)
                    .Where(x => x.Topic.Id == topicId)
                    .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}