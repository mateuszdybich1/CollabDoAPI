using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface ILeaderRepository
    {
        void AddLeader(LeaderEntity leader);

        Guid GetLeaderId(Guid userId);

        LeaderEntity GetLeader(Guid leaderId);

        Guid GetLeaderUserId(Guid leaderId);

        List<EmployeeDto> GetEmployees(Guid? leaderId);

        bool LeaderExists(Guid userId);

    }
}
