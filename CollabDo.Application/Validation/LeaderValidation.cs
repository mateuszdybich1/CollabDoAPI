using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Validation
{
    public class LeaderValidation
    {
        private readonly ILeaderRepository _leaderRepository;

        public LeaderValidation(ILeaderRepository leaderRepository)
        {
            _leaderRepository = leaderRepository;
        }

        public void ValidateLeader(Guid leaderId)
        {
            if(leaderId == Guid.Empty)
            {
                throw new ValidationException("Incorrect Leader ID");
            }
            if(!_leaderRepository.LeaderExists(leaderId))
            {
                throw new ValidationException("Incorrect Leader ID");
            }
        }
    }
}
