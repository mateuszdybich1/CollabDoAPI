namespace CollabDo.Application.Entities
{
    public class EmployeeEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public string? LeaderRequestEmail { get; set; }

        public Guid? LeaderId { get; set; }

        public LeaderEntity? Leader { get; set; }


        public EmployeeEntity() 
        { 

        }

        public EmployeeEntity(Guid userId) : base(userId)
        {
            
            UserId = userId;
        }
    }
}
