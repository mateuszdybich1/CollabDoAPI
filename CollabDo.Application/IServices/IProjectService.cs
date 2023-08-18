using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IServices
{
    public interface IProjectService
    {
        Guid SaveProject(ProjectDto projectDto);

        Guid UpdateProjectState(Guid projectId);

        Guid DeleteProject(Guid projectId);

        List<ProjectDto> GetProjects(ProjectStatus status, int pageNumber); 

        List<ProjectDto> GetProjects(Guid leaderId,ProjectStatus status, int pageNumber);
    }
}
