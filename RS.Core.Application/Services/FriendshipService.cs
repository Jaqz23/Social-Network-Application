using AutoMapper;
using Microsoft.AspNetCore.Http;
using RS.Core.Application.Dtos.Account;
using RS.Core.Application.Helpers;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Friendship;
using RS.Core.Domain.Entities;

namespace RS.Core.Application.Services
{
    public class FriendshipService : GenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _userSession;

        public FriendshipService(IFriendshipRepository friendshipRepository, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(friendshipRepository, mapper)
        {
            _friendshipRepository = friendshipRepository;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<FriendshipViewModel>> GetAllFriendsAsync()
        {
            if (_userSession == null) return new List<FriendshipViewModel>();

            var friendships = await _friendshipRepository.GetAllByUserIdAsync(_userSession.Id);
            var friendIds = friendships.Select(f => f.User_Id1 == _userSession.Id ? f.User_Id2 : f.User_Id1).ToList();
            var users = await _accountService.GetUsersByIdsAsync(friendIds);

            return users.Select(u => new FriendshipViewModel
            {
                User_Id2 = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePictureFriend = u.ProfilePicture
            }).ToList();
        }

        public async Task<(bool Success, string Message)> AddFriendAsync(string friendUsername)
        {
            if (_userSession == null)
            {
                return (false, "El usuario no está autenticado.");
            }

            var friend = await _accountService.GetUserByUsernameAsync(friendUsername);
            if (friend == null)
            {
                return (false, "El usuario ingresado no existe.");
            }

            if (friend.Id == _userSession.Id)
            {
                return (false, "No puedes agregarte a ti mismo como amigo.");
            }

            var existingFriendship = await _friendshipRepository.GetFriendshipAsync(_userSession.Id, friend.Id);
            if (existingFriendship != null)
            {
                return (false, "Ya tienes agregado a este usuario como amigo.");
            }

            var newFriendship = new Friendship
            {
                User_Id1 = _userSession.Id,
                User_Id2 = friend.Id
            };

            await _friendshipRepository.AddAsync(newFriendship);
            return (true, "Amigo agregado correctamente.");
        }


        public async Task<bool> RemoveFriendAsync(string friendId)
        {
            if (_userSession == null) return false;

            var friendship = await _friendshipRepository.GetFriendshipAsync(_userSession.Id, friendId);
            if (friendship == null) return false;

            await _friendshipRepository.DeleteAsync(friendship);
            return true;
        }

        public async Task<List<string>> GetFriendIdsByUserIdAsync(string userId)
        {
            var friendships = await _friendshipRepository.GetAllAsync();

            var friendIds = friendships
                .Where(f => f.User_Id1 == userId || f.User_Id2 == userId)
                .Select(f => f.User_Id1 == userId ? f.User_Id2 : f.User_Id1)
                .ToList();

            return friendIds;
        }

    }

}
