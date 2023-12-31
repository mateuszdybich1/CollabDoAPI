﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;

namespace CollabDo.Web.Repositories
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
        public EmployeeRequestEntity GetEmployeeRequest(string employeeEmail, Guid leaderId)
        {
            return _appDbContext.EmployeeRequests.SingleOrDefault(e => e.LeaderId == leaderId && e.Email == employeeEmail);
        }
        public EmployeeRequestEntity GetEmployeeRequest(Guid requestId)
        {
            return _appDbContext.EmployeeRequests
                .SingleOrDefault(e => e.Id == requestId);
        }
        public List<EmployeeRequestDto> GetEmployeeRequests(Guid leaderId)
        {
            List<EmployeeRequestEntity> entities = _appDbContext.EmployeeRequests
                .Where(e=>e.LeaderId == leaderId)
                .ToList();

            return entities.Select(EmployeeRequestDto.FromModel).ToList();
        }
    }
}
