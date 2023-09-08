using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;

namespace CollabDo.Application.Validation
{
    public class EmployeeValidation
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidation(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        public void ValidateEmployeeId(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ValidationException($"Employee does not exist");
            }
            if(!_employeeRepository.EmployeeExists(userId))
            {
                throw new ValidationException($"Employee does not exist");
            }
        }
    }
}
