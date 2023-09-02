using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(EmployeeEntity employee);

        void UpdateEmployee(EmployeeEntity employee);

        EmployeeEntity GetEmployee(Guid employeeId);

        bool EmployeeExists(Guid employeeId);

        Guid GetEmployeeUserId(Guid employeeId);

        Guid GetEmployeeId(Guid userId);

        Guid GetEmployeeId(Guid leaderId, Guid userId);
    }
}
