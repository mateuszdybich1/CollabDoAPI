using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public enum ProjectStatus
    {
        InProgress = 1,
        Finished = 2,
    }
    public class ProjectEntity : Entity<Guid>
    {
        public Priority Priority { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.InProgress;
        public List<AppUserEntity> Users { get; set; }
        public List<TaskEntity> Tasks { get; set; }

        public ProjectEntity() 
        { 

        }

        public ProjectEntity(Priority priority, ProjectStatus projectStatus)
        {
            Priority = priority;
            ProjectStatus = projectStatus;
        }
    }
}
