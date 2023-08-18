using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface IUserService
    {
        Task<Guid> Register(UserRegisterDto userDto);

        bool IsUserLeader();
    }
}
