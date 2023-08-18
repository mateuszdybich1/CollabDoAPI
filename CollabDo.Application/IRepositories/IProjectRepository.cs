using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface IProjectRepository
    {
        void AddProject(ProjectEntity project);

        void UpdateProject(ProjectEntity projectEntity);

        bool ProjectExists( Guid projectId);

        ProjectEntity GetProject(Guid projectId, Guid leaderId);

        List<ProjectDto> GetLeaderProjects(Guid leaderId, ProjectStatus status, int pageNumber);
    }
}
