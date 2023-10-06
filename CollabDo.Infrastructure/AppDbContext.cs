using CollabDo.Application.Entities;
using CollabDo.Web.EnityMappings;
using Microsoft.EntityFrameworkCore;

namespace CollabDo.Web
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<LeaderEntity> Leaders { get; set; }

        public DbSet<EmployeeRequestEntity> EmployeeRequests { get; set; }

        public DbSet<EmployeeEntity> Employees { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapAppUserEntity();

            modelBuilder.MapRelations();
        }
    }
}
