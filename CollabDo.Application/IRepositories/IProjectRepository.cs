using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface IProjectRepository
    {
        void AddProject(ProjectEntity project);
        void UpdateProject(ProjectEntity projectEntity);
        bool ProjectExists(Guid projectId);
        ProjectEntity GetProject(Guid projectId, Guid leaderId);
        List<ProjectEntity> GetLeaderProjects(Guid leaderId);

    }
}
