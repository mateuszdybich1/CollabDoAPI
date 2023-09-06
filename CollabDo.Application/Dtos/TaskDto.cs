using CollabDo.Application.Entities;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Application.Dtos
{
    public class TaskDto
    {
        public Guid? TaskId { get; private set; }

        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required] 
        public string Description { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public Entities.TaskStatus? Status { get; private set; } = Entities.TaskStatus.Started;

        public Guid? AssignedId { get; private set; }

        [Required]
        public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(1);


        public static TaskDto FromModel(TaskEntity entity)
        {
            TaskDto dto = new TaskDto();
            dto.TaskId = entity.Id;
            dto.ProjectId = entity.ProjectID;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.Priority = entity.Priority;
            dto.Status = entity.Status;
            dto.AssignedId = entity.AssignedUserId;
            dto.Deadline = entity.Deadline;
            return dto;
        }
    }
}
