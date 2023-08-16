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
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddProject(ProjectEntity project)
        {
            _appDbContext.Projects.Add(project);
            _appDbContext.SaveChanges();
        }
        public void UpdateProject(ProjectEntity projectEntity)
        {
            _appDbContext.Projects.Update(projectEntity);
            _appDbContext.SaveChanges();
        }
        public ProjectEntity GetProject(Guid projectId, Guid leaderId)
        {
            return _appDbContext.Projects.Include(e => e.Tasks).SingleOrDefault(e => e.Id == projectId && e.LeaderId == leaderId);
        }

        public List<ProjectEntity> GetLeaderProjects(Guid leaderId)
        {
            throw new NotImplementedException();
        }

        public bool ProjectExists(Guid leaderId,Guid projectId)
        {
            return _appDbContext.Projects.Any(e=>e.Id == projectId && e.LeaderId == leaderId);
        }
    }
}
