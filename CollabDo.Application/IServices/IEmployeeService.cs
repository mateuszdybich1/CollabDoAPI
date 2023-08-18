﻿using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface IEmployeeService
    {
        EmployeeDto GetEmployee();

        Task<Guid> CreateLeaderAssignmentRequest(string leaderEmail);

        Task<Guid> DeleteLeaderAssignmentRequest(string leaderEmail);
    }
}
