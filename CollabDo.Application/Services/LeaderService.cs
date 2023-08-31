using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;

namespace CollabDo.Application.Services
{
    public class LeaderService : ILeaderService
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRequestRepository _employeeRequestRepository;

        public LeaderService(IUserContext userContext, IUserRepository userRepository, IEmployeeRepository employeeRepository, ILeaderRepository leaderRepository, IEmployeeRequestRepository employeeRequestRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _leaderRepository = leaderRepository;
            _employeeRequestRepository = employeeRequestRepository;
        }


        public async Task<Guid> ApproveEmployeeRequest(Guid employeeRequestId, string employeeEmail)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(userId);

            Guid employeeId = await _userRepository.GetUserIdByEmail(employeeEmail);

            EmployeeRequestEntity employeeRequestEntity = _employeeRequestRepository.GetEmployeeRequest(employeeRequestId,employeeEmail);

            _employeeRequestRepository.DeleteEmployeeRequest(employeeRequestEntity);

            LeaderEntity leaderEntity = _leaderRepository.GetLeader(leaderId);

            EmployeeEntity employeeEntity = _employeeRepository.GetEmployee(employeeId);
            employeeEntity.LeaderId = leaderId;
            employeeEntity.Leader = leaderEntity;
            employeeEntity.ModifiedBy = userId;
            employeeEntity.ModifiedOn = DateTime.UtcNow;

            _employeeRepository.UpdateEmployee(employeeEntity);

            return employeeEntity.Id;
        }

        public async Task<Guid> RemoveEmployeeFromProject(Guid employeeRequestId, string employeeEmail)
        {
            Guid userId = _userContext.CurrentUserId;

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(userId);

            Guid employeeId = await _userRepository.GetUserIdByEmail(employeeEmail);

            EmployeeRequestEntity employeeRequestEntity = _employeeRequestRepository.GetEmployeeRequest(employeeRequestId, employeeEmail);

            _employeeRequestRepository.DeleteEmployeeRequest(employeeRequestEntity);

            EmployeeEntity employeeEntity = _employeeRepository.GetEmployee(employeeId);
            employeeEntity.LeaderRequestEmail = "";
            employeeEntity.LeaderId = null;
            employeeEntity.Leader = null;
            employeeEntity.ModifiedBy = userId;
            employeeEntity.ModifiedOn = DateTime.UtcNow;

            _employeeRepository.UpdateEmployee(employeeEntity);

            return employeeEntity.Id;
        }

        public List<EmployeeRequestDto> GetEmployeeRequests()
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(userId);

            return _employeeRequestRepository.GetEmployeeRequests(leaderId);
        }

        public List<EmployeeDto> GetEmployees()
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(userId);

            return _leaderRepository.GetEmployees(leaderId);
        }

        public List<EmployeeDto> GetEmployees(Guid? leaderId)
        {
            if(leaderId == null)
            {
                throw new ValidationException("leader ID is empty");
            }
            return _leaderRepository.GetEmployees(leaderId);

        }
    }
}
