using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
    }
    public enum ProjectStatus
    {
        InProgress = 1,
        Finished = 2,
    }
    public class ProjectEntity : Entity
    {
        public Priority Priority { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.InProgress;
        public List<AppUserEntity> Users { get; set; }
        public List<TaskEntity> Tasks { get; set; }

        public ProjectEntity() { }
        
    }
}
