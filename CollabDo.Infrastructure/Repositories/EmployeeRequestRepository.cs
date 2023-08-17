using CollabDo.Application.Dtos;
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

        public void DeleteEmployeeRequest(EmployeeRequestEntity employeeRequestEntity)
        {
            _appDbContext.EmployeeRequests.Remove(employeeRequestEntity);
            _appDbContext.SaveChanges();
        }
        public EmployeeRequestEntity GetEmployeeRequest(Guid leaderId)
        {
            return _appDbContext.EmployeeRequests.SingleOrDefault(e => e.LeaderId == leaderId);
        }
        public EmployeeRequestEntity GetEmployeeRequest(Guid employeeRequestId, string employeeEmail)
        {
            return _appDbContext.EmployeeRequests.SingleOrDefault(e => e.Id == employeeRequestId && e.Email == employeeEmail);
        }
        public List<EmployeeRequestDto> GetEmployeeRequests(Guid leaderId)
        {
            List<EmployeeRequestEntity> entities = _appDbContext.EmployeeRequests.Where(e=>e.LeaderId == leaderId).ToList();

            return entities.Select(EmployeeRequestDto.FromModel).ToList();
        }
    }
}
