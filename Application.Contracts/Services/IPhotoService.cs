using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IPhotoService 
    {
        Task<PhotoDto> GetById(Guid photoId, bool trackChanges, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<PhotoDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);

        Task CreateAsync(PhotoDto photoForCreation, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(PhotoDto photoForDeletion, CancellationToken cancellationToken = default);
       
        Task UpdateAsync(PhotoDto photoForUpdation, CancellationToken cancellationToken = default);
    }
}