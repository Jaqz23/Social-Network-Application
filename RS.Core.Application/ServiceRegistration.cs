using Microsoft.Extensions.DependencyInjection;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.Services;
using System.Reflection;


namespace RS.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Servicios
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            #endregion
        }
    }
}
