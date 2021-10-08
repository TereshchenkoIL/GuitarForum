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

        public ICategoryService CategoryService
        {
            get
            {
                if (_categoryService == null)
                    _categoryService = new CategoryService(_unitOfWork, _mapper);
                return _categoryService;
            }
        }

        public ICommentService CommentService
        {
            get
            {
                if (_commentService == null)
                    _commentService = new CommentService(_unitOfWork, _mapper, _userAccessor);
                return _commentService;
            }
        }

        public ILikeService LikeService
        {
            get
            {
                if (_likeService == null)
                    _likeService = new LikeService(_unitOfWork, _mapper, _userAccessor);
                return _likeService;
            }
        }
        public IPhotoService PhotoService { 
            get
            {
                if (_photoService == null)
                    _photoService = new PhotoService(_unitOfWork, _mapper, _userAccessor, _photoAccessor);
                return _photoService;
            } 
        }

        public IProfileService ProfileService
        {
            get
            {
                if (_profileService == null)
                    _profileService = new ProfileService(_unitOfWork, _mapper, _userAccessor);
                return _profileService;
            }
        }

        public ITopicService TopicService
        {
            get
            {
                if (_topicService == null)
                    _topicService = new TopicService(_unitOfWork, _mapper);
                return _topicService;
            }
        }
    }
}