using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Roles;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{

    //add-migration CamelCase -Context "MyContext"
    //update-database
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            //What this line does?
            ICurrentUserService currentUserService,
            IDateTime dateTime):base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<RequestItem> Requests { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<QA> QA { get; set; }
        public DbSet<ProjectOwner> ProjectOwner { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
