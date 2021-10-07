using System;
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
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken: cancellationToken)
                : await Context.Comments
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken: cancellationToken);
        }
    }
}