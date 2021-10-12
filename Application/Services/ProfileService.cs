using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Exceptions.ProfileExceptions;
using Domain.Exceptions.UserException;
using Domain.Repositories;
using Profile = Contracts.Profile;

namespace Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Profile> GetDetails(string username, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(username,  cancellationToken);

            if (user == null) throw new UserNotFound(username);

            return _mapper.Map<Profile>(user);
        }

      

        public async Task<Profile> UpdateAsync(string displayName, string bio, CancellationToken cancellationToken = default)
        {
            var username = _userAccessor.GetUsername();

           return await UpdateAsync(username, displayName, bio, cancellationToken);

        }

        public async Task<Profile> UpdateAsync(string username, string displayName, string bio, CancellationToken cancellationToken = default)
        {
           
            var user = await _unitOfWork.UserRepository.GetByUsername(username, cancellationToken );

            if (!string.IsNullOrEmpty(displayName))
            {
                user.DisplayName = displayName;
            }

            if (!string.IsNullOrEmpty(bio))
            {
                user.Bio = bio;
            }

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new ProfileUpdateException("Problem updating user");

            return _mapper.Map<Profile>(user);
        }
    }
}