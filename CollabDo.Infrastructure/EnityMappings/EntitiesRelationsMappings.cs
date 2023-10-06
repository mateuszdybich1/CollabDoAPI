using CollabDo.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Web.EnityMappings
{
    internal static class EntitiesRelationsMappings
    {
        internal static void MapRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaderEntity>()
                .HasMany(e => e.EmployeeRequests)
                .WithOne(e => e.Leader)
                .HasForeignKey(e=>e.LeaderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeaderEntity>()
                .HasMany(e=>e.Employees)
                .WithOne(e=>e.Leader)
                .HasForeignKey(e=>e.LeaderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaderEntity>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.Leader)
                .HasForeignKey(e=>e.LeaderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(e=>e.Tasks)
                .WithOne(e => e.Project)
                .HasForeignKey(e=>e.ProjectID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskEntity>()
                .HasMany(e=>e.Comments)
                .WithOne(e=>e.Task)
                .HasForeignKey(e=>e.TaskId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
