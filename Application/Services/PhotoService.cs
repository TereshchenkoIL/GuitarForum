using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.PhotoExceptions;
using Domain.Exceptions.UserException;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }
        public async Task<PhotoDto> GetById(string photoId, CancellationToken cancellationToken = default)
        {
            var photo = await _unitOfWork.PhotoRepository.GetById(photoId,  cancellationToken);

            if (photo == null) throw new PhotoNotFoundException(photoId);

            return _mapper.Map<PhotoDto>(photo);

        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var photos = await _unitOfWork.PhotoRepository.GetAllAsync( cancellationToken);

            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async  Task<PhotoDto> CreateAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var username = _userAccessor.GetUsername();

            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(username,  cancellationToken);
            if (user == null) throw new UserNotFound(username);
            if(user.Photo != null)
                await DeleteAsync(user.Photo.Id, cancellationToken);
            
            var photoResult = await _photoAccessor.AddPhoto(file);

            var photoForCreation = new Photo
            {
                Id = photoResult.PublicId,
                Url = photoResult.Url
            };

            user.Photo = photoForCreation;
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new PhotoCreateException("Failed to add the photo");

            return _mapper.Map<PhotoDto>(photoForCreation);
            
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var photo = await _unitOfWork.PhotoRepository.GetById(id, cancellationToken);
            if (photo == null) throw new PhotoNotFoundException(id);
            
            _unitOfWork.PhotoRepository.Delete(photo);
            
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new PhotoDeleteException("Failed to delete the photo"); 
            
            var cloudinaryResult = await _photoAccessor.DeletePhoto(id);
            
            if (cloudinaryResult == null) throw new CloudinaryException();
           
           
            
           
        }
    }
}