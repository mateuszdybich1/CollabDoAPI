using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    { 
        Task<Guid> ApproveEmployeeRequest(Guid requestId);

        Task<Guid> RemoveEmployeeFromProject(Guid requestId);

        Task<string> GetLederEmail(Guid leaderId);

        List<EmployeeRequestDto> GetEmployeeRequests();

        Task<List<EmployeeDto>> GetEmployees();

        Task<List<EmployeeDto>> GetEmployees(Guid? leaderId);
    }
}
