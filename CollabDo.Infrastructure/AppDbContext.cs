using CollabDo.Application.Entities;
using CollabDo.Infrastructure.EnityMappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<AppUserEntity> AppUsers { get; set; }

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
