using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Roles;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<RequestItem> Requests { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<QA> QA { get; set; }
        public DbSet<ProjectOwner> ProjectOwner { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
