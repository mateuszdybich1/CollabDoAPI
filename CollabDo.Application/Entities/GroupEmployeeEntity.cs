using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public class GroupEmployeeEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public Guid LeaderId { get; private set; }

        public GroupLeaderEntity Leader { get; private set; }


        public GroupEmployeeEntity() 
        { 

        }

        public GroupEmployeeEntity(Guid userId )
        {
            
            UserId = userId;
        }
    }
}
