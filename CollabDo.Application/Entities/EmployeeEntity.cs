using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public class EmployeeEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public string LeaderRequestEmail { get; set; }

        public Guid LeaderId { get; set; }

        public LeaderEntity Leader { get; private set; }


        public EmployeeEntity() 
        { 

        }

        public EmployeeEntity(Guid userId) : base(userId)
        {
            
            UserId = userId;
        }
    }
}
