using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
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

        public void AddLeader(GroupLeaderEntity leader)
        {
            _appDbContext.Leaders.Add(leader);
            _appDbContext.SaveChanges();
        }
    }
}
