using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public class EmployeeRequestEntity : Entity<Guid>
    {
        public string Username {get;set;}

        public string Email { get;set;}

        public Guid LeaderId { get;set;}
        public GroupLeaderEntity Leader { get;set;}
    }
}
