using AutoMapper;
using Contracts.Interfaces;
using Contracts.Services;
using Domain.Repositories;

namespace Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private ILikeService _likeService;
        private IPhotoService _photoService;
        private IProfileService _profileService;
        private ITopicService _topicService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }

        public ICategoryService CategoryService => _categoryService ??= new CategoryService(_unitOfWork, _mapper);
       

        public ICommentService CommentService =>
            _commentService ??= new CommentService(_unitOfWork, _mapper);
        

        public ILikeService LikeService => _likeService ??= new LikeService(_unitOfWork, _mapper, _userAccessor);

        public IPhotoService PhotoService => 
            _photoService ??= new PhotoService(_unitOfWork, _mapper, _userAccessor, _photoAccessor);

        public IProfileService ProfileService => _profileService ?? (_profileService = new ProfileService(_unitOfWork, _mapper, _userAccessor));

        public ITopicService TopicService => _topicService ?? (_topicService = new TopicService(_unitOfWork, _mapper, _userAccessor));
    }
}