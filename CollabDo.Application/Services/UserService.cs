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
        private readonly IUserContext _userContext;
        
        public UserService(ILeaderRepository leaderRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository, IUserContext userContext)
        {
            _leaderRepository = leaderRepository;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        
        public async Task<Guid> Register(UserRegisterDto userDto)
        {
            UserValidation validation = new UserValidation(_userRepository);
            await validation.ValidateEmail(userDto.Email);
            await validation.ValidateUsername(userDto.Username);

            

            UserEntity user = new UserEntity(userDto.Username,userDto.Email,userDto.Password);

            Guid userId = await _userRepository.AddUser(user);

            if (userDto.IsLeader)
            {
                LeaderEntity leader = new LeaderEntity(userId);
                 _leaderRepository.AddLeader(leader);

                return leader.Id;
                
            }

            EmployeeEntity employee = new EmployeeEntity(userId); 
            _employeeRepository.AddEmployee(employee);
            
            return employee.Id;
        }

        public bool IsUserLeader()
        {
            Guid userId = _userContext.CurrentUserId;

            return _leaderRepository.LeaderExists(userId);
        }

    }
}
