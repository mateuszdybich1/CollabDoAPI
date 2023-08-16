using CollabDo.Application.Dtos;
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
    }
}
