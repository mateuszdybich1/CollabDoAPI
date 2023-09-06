namespace CollabDo.Application.Entities
{
    
    public enum TaskStatus
    {
        Undone,
        Started,
        WaitingForApprovement,
        Done

    }
    public class TaskEntity : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public TaskStatus Status { get; private set; } = TaskStatus.Started;

        public Guid AssignedUserId { get; private set; } = Guid.Empty;
        
        public DateTime Deadline { get; set; } = DateTime.UtcNow.AddDays(1);

        public Guid ProjectID { get; private set; }

        public ProjectEntity Project { get; private set; }

        public List<CommentEntity> Comments { get; set; }

        public TaskEntity()
        {

        }
        public TaskEntity(Guid projectId, string name, string description, Priority priority, DateTime deadline, Guid userId) : base(userId) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            ProjectID = projectId;
            Name = name;
            Description = description;
            Priority = priority;
            Deadline = deadline;
        }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
        }

        public void AssignToEmployee(Guid employeeID)
        {
            AssignedUserId = employeeID;
        }



    }
}
