using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    
    public class LeaderEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public List<EmployeeRequestEntity> EmployeeRequests { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
        public List<ProjectEntity> Projects { get; set; }

        public LeaderEntity() 
        { 
        }

        public LeaderEntity(Guid userId) : base(userId)
        {

            UserId = userId;
        }

    }
}
