using RS.Core.Application.Dtos.Account;
using RS.Core.Application.ViewModels.User;
using RS.Core.Application.Wrappers;

namespace RS.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<List<AuthenticationResponse>> GetUsersByIdsAsync(List<string> userIds);
        Task<OperationResult> UpdateProfileAsync(string userId, ProfileViewModel vm);
        Task<AuthenticationResponse?> GetSingleUserByIdAsync(string userId);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<AuthenticationResponse?> GetUserByUsernameAsync(string username);
    }
}