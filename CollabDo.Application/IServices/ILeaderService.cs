using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    { 
        List<EmployeeRequestDto> GetEmployeeRequests();

        Task<Guid> ApproveEmployeeRequest(Guid employeeRequestId, string employeeEmail);
    }
}
