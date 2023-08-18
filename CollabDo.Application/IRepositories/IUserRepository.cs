using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface IUserRepository
    {
        Task<Guid> AddUser(UserEntity user);

        Task<KeycloakUserRequestModel> GetUser(Guid userId);

        Task<Guid> GetUserIdByEmail(string email);

        Task<bool> UsernameExists(string username);

        Task<bool> EmailExists(string email);     
    }
}
