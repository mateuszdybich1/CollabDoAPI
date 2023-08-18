using CollabDo.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Infrastructure.EnityMappings
{
    internal static class AppUserEntityMappings
    {
        internal static void MapAppUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaderEntity>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<LeaderEntity>().Property(e => e.UserId).IsRequired(true);

            modelBuilder.Entity<EmployeeEntity>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<EmployeeEntity>().Property(e => e.UserId).IsRequired(true);
        }
    }
}
