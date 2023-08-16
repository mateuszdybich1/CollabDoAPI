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

        public Guid LeaderId { get; private set; }

        public LeaderEntity Leader { get; private set; }


        public EmployeeEntity() 
        { 

        }

        public EmployeeEntity(Guid userId, Guid leaderId )
        {
            
            UserId = userId;
            LeaderId = leaderId;
        }
    }
}
