using BugTracker.DB;
using BugTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public class ProjectOwnerPersistance : IProjectOwnerPersistance
    {
        private AppDbContext _ctx;
        public ProjectOwnerPersistance(AppDbContext context)
        {
            _ctx = context;
        }

        public async Task<ProjectOwner> GetProjectOwner(string userId)
        {
            return await _ctx.ProjectOwner.FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public ProjectOwner GetProjectOwnerWithRelatedProjects(string userId)
        {
            return _ctx.ProjectOwner
             .Include(po => po.Projects)
             .Where(e =>e.UserId==userId)
             .SingleOrDefault();
        }

        public List<Project> GetRelatedProjects(int poId)
        {
            return _ctx.ProjectOwner
                .Include(po => po.Projects)
                .ThenInclude(p => p.Tickets)
                .Where(e => EF.Property<int>(e, "Id") == poId)
                .SingleOrDefault()
                .Projects;
        }

        public async Task SaveAsync(ProjectOwner po)
        {
            await _ctx.ProjectOwner.AddAsync(po);
            _ctx.SaveChanges();
        }
    }
}
