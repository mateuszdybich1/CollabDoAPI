using CollabDo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
