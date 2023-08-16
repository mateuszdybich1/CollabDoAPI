using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface IUserRepository
    {
        Task<Guid> AddUser(UserEntity user);
        Task<bool> UsernameExists(string username);
        Task<bool> EmailExists(string email);
        Task<Guid> GetUserIdByEmail(string email);
    }
}
