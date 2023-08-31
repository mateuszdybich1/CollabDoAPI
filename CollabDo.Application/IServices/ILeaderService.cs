using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    { 
        Task<Guid> ApproveEmployeeRequest(EmployeeRequestDto dto);

        Task<Guid> RemoveEmployeeFromProject(EmployeeRequestDto dto);

        List<EmployeeRequestDto> GetEmployeeRequests();

        List<EmployeeDto> GetEmployees();

        List<EmployeeDto> GetEmployees(Guid? leaderId);
    }
}
