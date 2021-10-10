using API.DTO;
using Contracts;
using Domain.Entities;
using Profile = AutoMapper.Profile;
namespace Application.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Like, LikeDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<LikeDto, Like>();
            CreateMap<PhotoDto, Photo>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photo.Url));

            CreateMap<Topic, TopicDto>()
                .ForMember(d => d.Likes, o => o.MapFrom(t => t.Likes.Count));
            CreateMap<TopicDto, Topic>()
                .ForMember(d => d.Likes, o=> o.Ignore());

            CreateMap<Topic, UserTopicDto>()
                .ForMember(d => d.CreatorUsername, o => o.MapFrom(t => t.Creator.UserName))
                .ForMember(d => d.TopicId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category));

            CreateMap<AppUser, Contracts.Profile>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Photo.Url))
                .ForMember(d => d.TopicsCount, o => o.MapFrom(u => u.Topics.Count));
            

        }
    }
}