using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;

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
