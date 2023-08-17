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
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IUserContext userContext, IEmployeeRepository employeeRepository)
        {
            _userContext = userContext;
            _employeeRepository = employeeRepository;
        }

        public Guid CreateLeaderAssignmentRequest(Guid leaderId)
        {
            throw new NotImplementedException();
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
    }
}
