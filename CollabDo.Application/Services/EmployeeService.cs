using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;

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
            employeeValidation.ValidateEmployeeId(userId);

            EmployeeEntity entity = _employeeRepository.GetEmployee(employeeId);

            EmployeeDto dto = EmployeeDto.FromModel(entity);
            
            return dto;
        }

        public async Task<Guid> CreateLeaderAssignmentRequest(string leaderEmail)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(userId);

            KeycloakUserRequestModel employeeUserData = await _userRepository.GetUser(userId);                   

            Guid leaderUserId = await _userRepository.GetUserIdByEmail(leaderEmail);
            Guid leaderId = _leaderRepository.GetLeaderId(leaderUserId);

            EmployeeRequestEntity employeeRequestEntity = new EmployeeRequestEntity(leaderId,employeeUserData.Username,employeeUserData.Email, userId);

            _employeeRequestRepository.AddEmployeeRequest(employeeRequestEntity);

            EmployeeEntity employeeEntity = _employeeRepository.GetEmployee(employeeId);
            employeeEntity.LeaderRequestEmail = leaderEmail;
            employeeEntity.ModifiedBy = userId;
            employeeEntity.ModifiedOn = DateTime.UtcNow;

            _employeeRepository.UpdateEmployee(employeeEntity);


            return employeeRequestEntity.Id;
        }
        
        public async Task<Guid> DeleteLeaderAssignmentRequest(string leaderEmail)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(userId);

            Guid leaderUserId = await _userRepository.GetUserIdByEmail(leaderEmail);

            Guid leaderId = _leaderRepository.GetLeaderId(leaderUserId);

            KeycloakUserRequestModel employeeUser = await _userRepository.GetUser(userId);

            EmployeeEntity employeeEntity = _employeeRepository.GetEmployee(employeeId);
            employeeEntity.LeaderRequestEmail = "";
            employeeEntity.LeaderId = null;
            employeeEntity.Leader = null;
            employeeEntity.ModifiedBy = userId;
            employeeEntity.ModifiedOn = DateTime.UtcNow;

            _employeeRepository.UpdateEmployee(employeeEntity);

            EmployeeRequestEntity employeeRequestEntity = _employeeRequestRepository.GetEmployeeRequest(employeeUser.Email, leaderId);

            if(employeeRequestEntity == null) 
            {
                throw new EntityNotFoundException("Request not found");
            }

            _employeeRequestRepository.DeleteEmployeeRequest(employeeRequestEntity);

            

            return employeeEntity.Id;
        }

        public Guid RemoveLeaderFromEmployee()
        {
            Guid userId = _userContext.CurrentUserId;
            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(userId);

            EmployeeEntity employeeEntity = _employeeRepository.GetEmployee(employeeId);
            employeeEntity.LeaderRequestEmail = "";
            employeeEntity.LeaderId = null;
            employeeEntity.ModifiedBy = userId;
            employeeEntity.ModifiedOn = DateTime.UtcNow;

            _employeeRepository.UpdateEmployee(employeeEntity);


            return employeeEntity.Id;
        }
    }
}
