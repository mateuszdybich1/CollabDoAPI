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


        public void ValidateLeader(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ValidationException("Incorrect Leader ID");
            }
            if(!_leaderRepository.LeaderExists(userId))
            {
                throw new ValidationException("Incorrect Leader ID");
            }
        }
    }
}
