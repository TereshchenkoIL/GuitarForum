using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
         ICategoryRepository CategoryRepository { get; }
         
         ICommentRepository CommentRepository { get; }
         
         ILikeRepository LikeRepository { get; }
         
         IPhotoRepository PhotoRepository { get; }
         
         ITopicRepository TopicRepository { get; }

         Task<bool> SaveChangesAsync();
    }
}