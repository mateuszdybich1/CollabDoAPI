using CollabDo.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Infrastructure.EnityMappings
{
    public static class EmployeeEntityMappings
    {
        public static void MapEmployeeEntity(this ModelBuilder builder)
        {
            builder.Entity<EmployeeRequestEntity>().HasIndex(e => e.LeaderId).IsUnique();
            builder.Entity<EmployeeRequestEntity>().Property(e => e.Username).IsRequired(true);
            builder.Entity<EmployeeRequestEntity>().Property(e => e.Email).IsRequired(true);
        }
    }
}
