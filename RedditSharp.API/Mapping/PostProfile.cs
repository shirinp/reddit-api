using AutoMapper;
using RedditSharp.API.ViewModel;
using RedditSharp.Things;

namespace RedditSharp.API.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile() {
            CreateMap<Post, PostModel>()
                .ForMember(dest => dest.SubRedditName, opt => opt.MapFrom(src => src.SubredditName))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UpVoteCount, opt => opt.MapFrom(src => src.Upvotes))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserModel() { UserName = src.AuthorName }))
                .ForMember(dest => dest.PostedAtUtc, opt => opt.MapFrom(src => src.CreatedUTC));

            CreateMap<IGrouping<string?, PostModel>, UserPostModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserModel() { UserName = src.Key }))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.ToList() ));
        }
    }
}
