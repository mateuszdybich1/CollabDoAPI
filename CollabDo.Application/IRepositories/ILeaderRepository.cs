using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface ILeaderRepository
    {
        void AddLeader(LeaderEntity leader);

        Guid GetLeaderId(Guid userId);

        List<EmployeeDto> GetEmployees(Guid leaderId);

        bool LeaderExists(Guid leaderId);

    }
}
