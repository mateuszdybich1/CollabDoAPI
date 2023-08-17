using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface IEmployeeRequestRepository
    {
        void AddEmployeeRequest(EmployeeRequestEntity employeeRequestEntity);

        void DeleteEmployeeRequest(EmployeeRequestEntity employeeRequestEntity);

        EmployeeRequestEntity GetEmployeeRequest(Guid leaderId);

        EmployeeRequestEntity GetEmployeeRequest(Guid employeeRequestId, string employeeEmail);

        List<EmployeeRequestDto> GetEmployeeRequests(Guid leaderId);
    }
}
