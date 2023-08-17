using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
