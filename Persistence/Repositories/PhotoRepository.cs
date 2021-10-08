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

        public async Task<Photo> GetById(string photoId,  CancellationToken cancellationToken = default)
        {
            return await Context.Photos
                    .FirstOrDefaultAsync(x => x.Id == photoId, cancellationToken: cancellationToken);

        }
    }
}