using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contracts.Services
{
    public interface IPhotoService 
    {
        Task<PhotoDto> GetById(string photoId, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<PhotoDto>> GetAllAsync( CancellationToken cancellationToken = default);

        Task<PhotoDto> CreateAsync(IFormFile file, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(string Id, CancellationToken cancellationToken = default);
       
      
    }
}