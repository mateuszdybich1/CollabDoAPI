using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface ILeaderRepository
    {
        void AddLeader(LeaderEntity leader);

        Guid GetLeaderId(Guid userId);

        bool LeaderExists(Guid leaderId);
    }
}
