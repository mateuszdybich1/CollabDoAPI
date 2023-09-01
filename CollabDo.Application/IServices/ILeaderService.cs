using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    { 
        Task<Guid> ApproveEmployeeRequest(Guid requestId);

        Task<Guid> RemoveEmployeeFromProject(Guid requestId);

        List<EmployeeRequestDto> GetEmployeeRequests();

        List<EmployeeDto> GetEmployees();

        List<EmployeeDto> GetEmployees(Guid? leaderId);
    }
}
