using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IServices
{
    public interface IProjectService
    {
        Guid SaveProject(ProjectDto projectDto);

        Guid UpdateProjectState(Guid projectId);

        List<ProjectDto> GetProjects(ProjectStatus status, int pageNumber); 

        List<ProjectDto> GetProjects(Guid leaderId,ProjectStatus status, int pageNumber);
    }
}
