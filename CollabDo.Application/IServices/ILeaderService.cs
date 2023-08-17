using CollabDo.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IServices
{
    public interface ILeaderService
    {
        
        List<EmployeeRequestDto> GetEmployeeRequests();

        Task<Guid> ApproveEmployeeRequest(Guid employeeRequestId, string employeeEmail);
    }
}
