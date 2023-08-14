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
        public ProjectStatus ProjectStatus { get; private set; } = ProjectStatus.InProgress;
        public Guid LeaderId { get; set; }
        public GroupLeaderEntity Leader { get; private set; }
        public List<TaskEntity> Tasks { get; set; }

        public ProjectEntity() 
        { 

        }

        public ProjectEntity(Guid leaderId, string name, Priority priority) : base(leaderId)
        {
            LeaderId = leaderId;
            Name = name;
            Priority = priority;
        }

        public void SetProjectStatus(ProjectStatus projectStatus)
        {
            ProjectStatus = projectStatus;
        }
    }
}
