using CollabDo.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IServices
{
    public interface IEmployeeService
    {
        EmployeeDto GetEmployee();

        Task<Guid> CreateLeaderAssignmentRequest(string leaderEmail);

        Task<Guid> DeleteLeaderAssignmentRequest(string leaderEmail);
    }
}
