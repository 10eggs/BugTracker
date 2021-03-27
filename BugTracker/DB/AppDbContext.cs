using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BugTracker.Models.TicketProperties;

namespace BugTracker.DB
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<QA> QA { get; set; }
        public DbSet<ProjectOwner> ProjectOwner { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Request>(
                b =>
                {
                    b.Property(r => r.Title)
                    .IsRequired();

                    b.Property(r => r.Description)
                    .IsRequired();

                    b.Property(r => r.Author)
                    .IsRequired();

                    b.HasOne(r => r.Project)
                    .WithMany(p => p.Requests);
                });


            modelBuilder.Entity<Ticket>(
                b =>
                {
                    b.Property(e => e.Title)
                    .IsRequired();

                    b.Property(e => e.Description)
                    .IsRequired();

                    b.Property(e => e.Author)
                    .IsRequired();

                    b.HasOne(e => e.Project)
                        .WithMany(e => e.Tickets);

                    b.Navigation(b => b.Project);

                    b.HasOne(e => e.Qa);

                    b.Property(at => at.TicketStatus)
                    .HasDefaultValue(TicketStatus.Open)
                    .HasConversion(at => at.ToString(),
                    at => (TicketStatus)Enum.Parse(typeof(TicketStatus), at));

                    b.Property(at => at.TicketPriority)
                    .HasDefaultValue(TicketPriority.None)
                    .HasConversion(at => at.ToString(),
                    at => (TicketPriority)Enum.Parse(typeof(TicketPriority), at));

                    b.Property(at => at.TicketCategory)
                    .HasDefaultValue(TicketCategory.None)
                    .HasConversion(at => at.ToString(),
                    at => (TicketCategory)Enum.Parse(typeof(TicketCategory), at));

                });

            modelBuilder.Entity<Project>(
                b =>
                {
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
                  b.HasMany(b => b.Projects)
                    .WithMany(p => p.QAs);
                  b.HasMany(b => b.Tickets)
                    .WithOne(t => t.Qa);
              });

            //add-migration CamelCase -Context "MyContext"
            //update-database
        }
    }
}
