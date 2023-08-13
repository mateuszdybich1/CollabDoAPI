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
            modelBuilder.Entity<AppUserEntity>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<AppUserEntity>().Property(e => e.UserId).IsRequired(true);

            modelBuilder.Entity<AppUserEntity>().HasIndex(e => e.Username).IsUnique();
            modelBuilder.Entity<AppUserEntity>().Property(e => e.Username).IsRequired(true);
        }
    }
}
