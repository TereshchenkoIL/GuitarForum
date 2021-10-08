namespace Contracts.Services
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        ICommentService CommentService { get; }
        ILikeService LikeService { get; }
        IPhotoService PhotoService { get; }
        IProfileService ProfileService { get; }
        ITopicService TopicService { get; }
    }
}