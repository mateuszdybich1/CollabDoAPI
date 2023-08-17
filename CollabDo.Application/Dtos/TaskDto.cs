using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class TaskDto
    {
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public Priority Priority { get; set; }

        

        public Entities.TaskStatus Status { get; private set; } = Entities.TaskStatus.Created;

        public Guid AssignedToEmployeeId { get; private set; }

        [Required]
        public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(1);


        public static TaskDto FromModel(TaskEntity entity)
        {
            TaskDto dto = new TaskDto();
            dto.ProjectId = entity.ProjectID;
            dto.Name = entity.Name;
            dto.Priority = entity.Priority;
            dto.Status = entity.Status;
            dto.AssignedToEmployeeId = entity.AssignedEmployeeId;
            dto.Deadline = entity.Deadline;
            return dto;
        }
    }
}
