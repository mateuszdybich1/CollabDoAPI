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
    }
}
