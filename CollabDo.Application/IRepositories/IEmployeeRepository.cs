using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(GroupEmployeeEntity employee);

        bool EmployeeExists(Guid employeeId);
        Guid GetEmployeeId(Guid userId);
        Guid GetEmployeeId(Guid leaderId, Guid userId);

    }
}
