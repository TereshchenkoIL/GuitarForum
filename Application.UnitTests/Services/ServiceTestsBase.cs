using Application.Mapping;
using Application.Services;
using AutoMapper;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Services
{
    public class ServiceTestsBase
    {
        protected IServiceManager _serviceManager;
        protected readonly IMapper _mapper;
        protected readonly Mock<IUnitOfWork> _unitOfWork;
        protected readonly Mock<IUserAccessor> _userAccesor;
        protected readonly Mock<IPhotoAccessor> _photoAccesor;
        protected readonly Mock<ICategoryRepository> _categoryRepository;
        protected readonly Mock<ITopicRepository> _topicRepository;
        protected readonly Mock<IUserRepository> _userRepository;
         
        public  ServiceTestsBase()
        {
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _categoryRepository = new Mock<ICategoryRepository>();
            _topicRepository = new Mock<ITopicRepository>();
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            PrepareRepository();
            _userAccesor = new Mock<IUserAccessor>();
            _photoAccesor = new Mock<IPhotoAccessor>();
            _serviceManager =
                new ServiceManager(_unitOfWork.Object, _mapper, _userAccesor.Object, _photoAccesor.Object);
        }

        private void PrepareRepository()
        {
            _unitOfWork.Setup(u => u.CategoryRepository).Returns(_categoryRepository.Object);
            _unitOfWork.Setup(u => u.TopicRepository).Returns(_topicRepository.Object);
            _unitOfWork.Setup(u => u.UserRepository).Returns(_userRepository.Object);
        }
        

    }
}