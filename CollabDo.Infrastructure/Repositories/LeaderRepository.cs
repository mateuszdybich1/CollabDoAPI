using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Infrastructure.Repositories
{
    public class LeaderRepository : ILeaderRepository
    {
        private readonly AppDbContext _appDbContext;

        public LeaderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddLeader(LeaderEntity leader)
        {
            _appDbContext.Leaders.Add(leader);
            _appDbContext.SaveChanges();
        }

        public Guid GetLeaderId(Guid userId)
        {
            return _appDbContext.Leaders.SingleOrDefault(e => e.UserId == userId).Id;
        }

        public bool LeaderExists(Guid leaderId)
        {
            return _appDbContext.Leaders.Any(e=>e.Id == leaderId);
        }

        public List<EmployeeDto> GetEmployees(Guid leaderId)
        {
            var leader = _appDbContext.Leaders.Where(e=>e.Id == leaderId).Include(e=>e.Employees).SingleOrDefault();

            return leader.Employees.Select(EmployeeDto.FromModel).ToList();
        }
    }
}
