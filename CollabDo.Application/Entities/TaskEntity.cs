
namespace CollabDo.Application.Entities
{
    
    public enum TaskStatus
    {
        Undone,
        Created,
        Started,
        WaitingForApprovement,
        Done

    }
    public class TaskEntity : Entity<Guid>
    {
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Created;
        public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(1);

        public Guid ProjectID { get; private set; }
        public ProjectEntity Project { get; private set; }

        public List<CommentEntity> Comments { get; set; }

        public TaskEntity()
        {

        }
        public TaskEntity(Priority priority, TaskStatus status, DateTime deadline)
        {
            Priority = priority;
            Status = status;
            Deadline = deadline;
        }
    }
}
