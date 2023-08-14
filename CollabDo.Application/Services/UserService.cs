using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        
        public UserService(ILeaderRepository leaderRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _leaderRepository = leaderRepository;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }
        public async Task<Guid> Register(UserRegisterDto userDto)
        {
            UserValidation validation = new UserValidation(_userRepository);
            await validation.ValidateEmail(userDto.Email);
            await validation.ValidateUsername(userDto.Username);

            UserRole role;

            if (userDto.IsLeader)
            {
                role = UserRole.Leader;
            }
            else 
            { 
                role = UserRole.Employee;
            }

            UserEntity user = new UserEntity(userDto.Username,userDto.Email,userDto.Password,role);

            Guid userId = await _userRepository.AddUser(user);

            if (userDto.IsLeader)
            {
                GroupLeaderEntity leader = new GroupLeaderEntity(userId);
                 _leaderRepository.AddLeader(leader);
                
            }

            return userId;
        }
    }
}
