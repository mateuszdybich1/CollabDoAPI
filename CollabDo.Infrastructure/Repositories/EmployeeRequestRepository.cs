using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure.Repositories
{
    public class EmployeeRequestRepository : IEmployeeRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddEmployeeRequest(EmployeeRequestEntity employeeRequestEntity)
        {
            _appDbContext.EmployeeRequests.Add(employeeRequestEntity);
            _appDbContext.SaveChanges();
        }
    }
}
