﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface IEmployeeRequestRepository
    {
        void AddEmployeeRequest(EmployeeRequestEntity employeeRequestEntity);

        void DeleteEmployeeRequest(EmployeeRequestEntity employeeRequestEntity);

        EmployeeRequestEntity GetEmployeeRequest(string employeeEmail, Guid leaderId);

        EmployeeRequestEntity GetEmployeeRequest(Guid requestId);

        List<EmployeeRequestDto> GetEmployeeRequests(Guid leaderId);
    }
}
