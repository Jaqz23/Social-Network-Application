using AutoMapper;
using RS.Core.Application.Dtos.Account;
using RS.Core.Application.ViewModels.Comment;
using RS.Core.Application.ViewModels.Friendship;
using RS.Core.Application.ViewModels.Post;
using RS.Core.Application.ViewModels.User;
using RS.Core.Domain.Entities;

namespace RS.Core.Application.Mappings
{
    public class GeneralProfile : Profile 
    {
        public GeneralProfile()
        {
            #region Authentication
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Register
            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture ?? "/Images/default-avatar.png"))
                .ReverseMap()
                .ForMember(x => x.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture ?? "/Images/default-avatar.png"));

            #endregion

            #region Forgot Password
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Reset Password
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion


            #region Post
            CreateMap<Post, SavePostViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())  // Ignorar archivo 
                .ReverseMap();

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) 
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) 
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments)) // Mapear comentarios
                .ReverseMap();
            #endregion

            #region Friendship
            CreateMap<Friendship, FriendshipViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) 
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureFriend, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Friendship, SaveFriendshipViewModel>()
                .ForMember(dest => dest.FriendUser, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Comments
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) 
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) 
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
                .ReverseMap();

            CreateMap<Comment, SaveCommentViewModel>()
                .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.UserID))
                .ReverseMap()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User_Id));
            #endregion


        }
    }
}
