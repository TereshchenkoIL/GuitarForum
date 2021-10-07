using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
         IUserRepository UserRepository { get; }
         ICategoryRepository CategoryRepository { get; }
         
         ICommentRepository CommentRepository { get; }
         
         ILikeRepository LikeRepository { get; }
         
         IPhotoRepository PhotoRepository { get; }
         
         ITopicRepository TopicRepository { get; }

         Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}