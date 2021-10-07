using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IPhotoService 
    {
        Task<PhotoDto> GetById(string photoId, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<PhotoDto>> GetAllAsync( CancellationToken cancellationToken = default);

        Task CreateAsync(PhotoDto photoForCreation, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(PhotoDto photoForDeletion, CancellationToken cancellationToken = default);
       
      
    }
}