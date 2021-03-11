using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.DB
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        //public AppDbContext(DbContextOptions<AppDbContext> options):
        //    base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectOwner> ProjectOwner { get; set; }

        public DbSet<QA> QA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(
                b =>
                {
                    //b.Property<int>("Id");
                    b.Property(e => e.Id);
                    b.Property(e => e.Title);
                    b.Property(e => e.Description);
                    b.Property(e => e.Author);
                    b.Property(e => e.Date);
                    b.HasOne(e => e.Project)
                        .WithMany(e => e.Tickets);

                    b.Navigation(b => b.Project);

                });

            modelBuilder.Entity<Project>(
                b =>
                {
                    //b.Property<int>("Id");
                    b.Property(e => e.Id);
                    b.HasOne(e => e.ProjectOwner)
                        .WithMany(e => e.Projects);

                    b.Navigation(e => e.ProjectOwner);

                });

            modelBuilder.Entity<ProjectOwner>(
                b =>
                {
                    b.Property(b => b.Id);
                    b.Property(b => b.UserId);
                    b.Property(b => b.Name);
                    b.HasMany(b => b.Projects);
                        
                });

            modelBuilder.Entity<QA>(
              b =>
              {
                b.Property(b => b.Id);
                b.Property(b => b.Name);
                b.HasMany(b => b.Projects);
                b.HasMany(b => b.Tickets);
              });

        }
    }
}
