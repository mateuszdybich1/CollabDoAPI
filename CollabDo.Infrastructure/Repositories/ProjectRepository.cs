using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteProject(ProjectEntity projectEntity)
        {
            _appDbContext.Projects.Remove(projectEntity);
            _appDbContext.SaveChanges();
        }

        public ProjectEntity GetProject(Guid projectId, Guid leaderId)
        {
            return _appDbContext.Projects.Include(e => e.Tasks).SingleOrDefault(e => e.Id == projectId && e.LeaderId == leaderId);
        }

        public List<ProjectDto> GetLeaderProjects(Guid leaderId, ProjectStatus status, int pageNumber)
        {
            List<ProjectEntity> entities = _appDbContext.Projects
                .Where(e => e.LeaderId == leaderId && e.ProjectStatus == status)
                .OrderByDescending(e => e.Priority)
                .Skip((pageNumber-1)*25)
                .Take(25)
                .ToList();

            return entities.Select(ProjectDto.FromModel).ToList();
        }

        public bool ProjectExists(Guid projectId)
        {
            return _appDbContext.Projects.Any(e=>e.Id == projectId);
        }
    }
}
