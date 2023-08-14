using CollabDo.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure.EnityMappings
{
    internal static class EntitiesRelationsMappings
    {
        internal static void MapRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupLeaderEntity>().HasMany(e => e.EmployeeRequests).WithOne(e => e.Leader).HasForeignKey(e=>e.LeaderId).IsRequired(true);

            modelBuilder.Entity<GroupLeaderEntity>().HasMany(e=>e.Employees).WithOne(e=>e.Leader).HasForeignKey(e=>e.LeaderId).IsRequired(false);

            modelBuilder.Entity<GroupLeaderEntity>().HasMany(e => e.Projects).WithOne(e => e.Leader).HasForeignKey(e=>e.LeaderId).IsRequired(true);

            modelBuilder.Entity<ProjectEntity>().HasMany(e=>e.Tasks).WithOne(e => e.Project).HasForeignKey(e=>e.ProjectID).IsRequired(false);

            modelBuilder.Entity<TaskEntity>().HasMany(e=>e.Comments).WithOne(e=>e.Task).HasForeignKey(e=>e.TaskId).IsRequired(false);
        }
    }
}
