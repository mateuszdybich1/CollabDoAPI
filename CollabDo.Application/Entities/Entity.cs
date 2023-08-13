
using System;

namespace CollabDo.Application.Entities
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public Guid CreatedBy { get; private set; }
        public Guid? ModifiedBy { get; set; }

        public Entity(Guid id)
        {
            Id = id;
        }

        protected Entity(Guid id, Guid createdBy)
        {
            Id = id;
            CreatedBy = createdBy;
        }

        protected Entity()
        {
        }
    }
}