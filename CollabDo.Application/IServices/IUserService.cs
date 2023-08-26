using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface IUserService
    {
        Task<Guid> Register(UserRegisterDto userDto);

        Task<bool> VerifyEmail();

        Task<bool> ResetPassword(string userEmail);

        bool IsUserLeader();
    }
}
