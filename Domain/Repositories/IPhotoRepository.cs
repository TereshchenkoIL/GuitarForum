using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPhotoRepository : IRepositoryBase<Photo>
    {
        Task<Photo> GetById(string photoId,  CancellationToken cancellationToken = default);
    }
}