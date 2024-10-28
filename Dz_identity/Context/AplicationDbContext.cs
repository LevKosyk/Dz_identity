using Dz_identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace Dz_identity.Context
{
        public class AplicationDbContext : IdentityDbContext<IdentityUser> // Change to IdentityDbContext
        {
            public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Project> Projects { get; set; }
            public DbSet<TaskModel> Tasks { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<TaskModel>()
                    .HasOne(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
}
