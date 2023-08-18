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

        
        public void ValidateEmployeeId(Guid employeeId)
        {
            if(employeeId == Guid.Empty)
            {
                throw new ValidationException($"Employee with ID: {employeeId} does not exist");
            }
            if(!_employeeRepository.EmployeeExists(employeeId))
            {
                throw new ValidationException($"Employee with ID: {employeeId} does not exist");
            }
        }
    }
}
