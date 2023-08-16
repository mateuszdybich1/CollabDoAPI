using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddEmployee(GroupEmployeeEntity employee)
        {
            _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
        }

        public bool EmployeeExists(Guid employeeId)
        {
            return _appDbContext.Employees.Any(e=>e.Id == employeeId);
        }


        public Guid GetEmployeeId(Guid leaderId, Guid userId)
        {
            return _appDbContext.Employees.SingleOrDefault(e=>e.LeaderId == leaderId && e.UserId == userId).Id;
        }

        public Guid GetEmployeeId(Guid userId)
        {
            return _appDbContext.Employees.SingleOrDefault(e => e.UserId == userId).Id;
        }
    }
}
