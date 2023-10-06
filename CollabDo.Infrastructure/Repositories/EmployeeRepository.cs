using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;

namespace CollabDo.Web.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public void AddEmployee(EmployeeEntity employee)
        {
            _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
        }

        public void UpdateEmployee(EmployeeEntity employee)
        {
            _appDbContext.Employees.Update(employee);
            _appDbContext.SaveChanges();
        }

        public EmployeeEntity GetEmployee(Guid employeeId)
        {
            return _appDbContext.Employees.SingleOrDefault(e => e.Id == employeeId);
        }

        public bool EmployeeExists(Guid userId)
        {
            return _appDbContext.Employees.Any(e=>e.UserId == userId);
        }


        public Guid GetEmployeeId(Guid leaderId, Guid userId)
        {
            return _appDbContext.Employees.SingleOrDefault(e=>e.LeaderId == leaderId && e.UserId == userId).Id;
        }

        public Guid GetEmployeeId(Guid userId)
        {
            return _appDbContext.Employees.SingleOrDefault(e => e.UserId == userId).Id;
        }

        public Guid GetEmployeeUserId(Guid employeeId)
        {
            return _appDbContext.Employees.SingleOrDefault(e=>e.Id==employeeId).UserId;
        }
    }
}
