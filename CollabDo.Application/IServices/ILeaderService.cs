using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    { 
        Task<Guid> ApproveEmployeeRequest(Guid employeeRequestId, string employeeEmail);

        Task<Guid> RemoveEmployeeFromProject(Guid employeeRequestId, string employeeEmail);

        List<EmployeeRequestDto> GetEmployeeRequests();

        List<EmployeeDto> GetEmployees();

        List<EmployeeDto> GetEmployees(Guid? leaderId);
    }
}
