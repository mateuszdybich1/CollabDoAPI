
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
        public string Name { get; set; }

        public Priority Priority { get; set; }

        public TaskStatus Status { get; private set; } = TaskStatus.Created;

        public Guid AssignedToEmployeeId { get; private set; }
        
        public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(1);

        public Guid ProjectID { get; private set; }

        public ProjectEntity Project { get; private set; }

        public List<CommentEntity> Comments { get; set; }

        public TaskEntity()
        {

        }
        public TaskEntity(Guid projectId, string name, Priority priority, DateTime deadline)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            ProjectID = projectId;
            Name = name;
            Priority = priority;
            Deadline = deadline;
        }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
        }

        public void AssignToEmployee(Guid employeeID)
        {
            AssignedToEmployeeId = employeeID;
        }
    }
}
