using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private ICategoryRepository _categoryRepository;
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;
        private IPhotoRepository _photoRepository;
        private ITopicRepository _topicRepository;
        private IUserRepository _userRepository;
        
        
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_context);
                return _categoryRepository;
            }
        }

        public ICommentRepository CommentRepository { 
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_context);
                return _commentRepository;
            } 
        }

        public ILikeRepository LikeRepository
        {
            get
            {
                if (_likeRepository == null)
                    _likeRepository = new LikeRepository(_context);
                return _likeRepository;
            }
        }

        public IPhotoRepository PhotoRepository
        {
            get
            {
                if (_photoRepository == null)
                    _photoRepository = new PhotoRepository(_context);
                return _photoRepository;
            }
        }

        public ITopicRepository TopicRepository
        {
            get
            {
                if (_topicRepository == null)
                    _topicRepository = new TopicRepository(_context);
                return _topicRepository;
            }
        }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}