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
        public string Name { get; set; }
        public Priority Priority { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.InProgress;
        public GroupLeaderEntity Leader { get; set; }
        public List<TaskEntity> Tasks { get; set; }

        public ProjectEntity() 
        { 

        }

        public ProjectEntity(string name, Priority priority, ProjectStatus projectStatus)
        {
            Name = name;
            Priority = priority;
            ProjectStatus = projectStatus;
        }
    }
}
