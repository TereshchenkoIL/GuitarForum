using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(DataContext context) : base(context)
        {
        }

        public async Task<Photo> GetById(string photoId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return !trackChanges
                ? await Context.Photos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == photoId, cancellationToken: cancellationToken)
                : await Context.Photos
                    .Include(x => x.Owner)
                    .FirstOrDefaultAsync(x => x.Id == photoId, cancellationToken: cancellationToken);

        }
    }
}