using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IPhotoService : IService<PhotoDto>
    {
        Task<PhotoDto> GetById(Guid photoId, bool trackChanges, CancellationToken cancellationToken = default);
    }
}