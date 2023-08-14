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
            modelBuilder.Entity<GroupLeaderEntity>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<GroupLeaderEntity>().Property(e => e.UserId).IsRequired(true);

            modelBuilder.Entity<GroupEmployeeEntity>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<GroupEmployeeEntity>().Property(e => e.UserId).IsRequired(true);
        }
    }
}
