using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.PhotoExceptions;
using Domain.Repositories;

namespace Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PhotoDto> GetById(string photoId, CancellationToken cancellationToken = default)
        {
            var photo = await _unitOfWork.PhotoRepository.GetById(photoId, false, cancellationToken);

            if (photo == null) throw new PhotoNotFoundException(photoId);

            return _mapper.Map<PhotoDto>(photo);

        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var photos = await _unitOfWork.PhotoRepository.GetAllAsync(false, cancellationToken);

            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task CreateAsync(PhotoDto photoForCreation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.PhotoRepository.Create(_mapper.Map<Photo>(photoForCreation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new PhotoCreateException("Failed to add the photo");
        }

        public async Task DeleteAsync(PhotoDto photoForDeletion, CancellationToken cancellationToken = default)
        {
            _unitOfWork.PhotoRepository.Create(_mapper.Map<Photo>(photoForDeletion));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new PhotoDeleteException("Failed to delete the photo");
        }
    }
}