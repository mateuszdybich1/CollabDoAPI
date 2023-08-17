using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface ILeaderRepository
    {
        void AddLeader(LeaderEntity leader);
        Guid GetLeaderId(Guid userId);
        bool LeaderExists(Guid leaderId);

        bool LeaderHasEmployee(Guid leaderId,Guid employeeId);
    }
}
