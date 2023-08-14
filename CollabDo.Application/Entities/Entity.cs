
using System;

namespace CollabDo.Application.Entities
{
    public class Entity<T>
    {
        public T Id { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public Guid CreatedBy { get; private set; }
        public Guid? ModifiedBy { get; set; }

        public Entity(T id)
        {
            Id = id;
        }
        public Entity(Guid createdBy)
        {
            CreatedBy = createdBy;
        }
        protected Entity(T id, Guid createdBy)
        {
            Id = id;
            CreatedBy = createdBy;
        }

        protected Entity()
        {
        }
    }
}