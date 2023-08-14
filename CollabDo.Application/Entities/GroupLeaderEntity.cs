using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    
    public class GroupLeaderEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public List<EmployeeRequestEntity> EmployeeRequests { get; set; }
        public List<GroupEmployeeEntity> Employees { get; set; }
        public List<ProjectEntity> Projects { get; set; }

        public GroupLeaderEntity() 
        { 
        }

        public GroupLeaderEntity(Guid userId)
        {
            

            UserId = userId;
        }

    }
}
