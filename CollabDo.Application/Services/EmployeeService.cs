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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRequestRepository _employeeRequestRepository;


        public EmployeeService(
            IUserContext userContext, 
            IUserRepository userRepository, 
            IEmployeeRepository employeeRepository, 
            ILeaderRepository leaderRepository, 
            IEmployeeRequestRepository employeeRequestRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _leaderRepository = leaderRepository;
            _employeeRequestRepository = employeeRequestRepository;
        }

       

        public EmployeeDto GetEmployee()
        {
            Guid userId = _userContext.CurrentUserId;

            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(employeeId);

            EmployeeEntity entity = _employeeRepository.GetEmployee(employeeId);

            EmployeeDto dto = new EmployeeDto();
            dto.LeaderRequestEmail = entity.LeaderRequestEmail;
            dto.LeaderId = entity.LeaderId;
            


            return dto;
        }


        public async Task<Guid> CreateLeaderAssignmentRequest(string leaderEmail)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            KeycloakUserRequestModel employeeUserData = await _userRepository.GetUser(userId);                   


            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(employeeId);


            Guid leaderUserId = await _userRepository.GetUserIdByEmail(leaderEmail);

            Guid leaderId = _leaderRepository.GetLeaderId(leaderUserId);

            EmployeeRequestEntity employeeRequestEntity = new EmployeeRequestEntity(leaderId,employeeUserData.Username,employeeUserData.Email, userId);

            _employeeRequestRepository.AddEmployeeRequest(employeeRequestEntity);

            return employeeRequestEntity.Id;



        }
    }
}
