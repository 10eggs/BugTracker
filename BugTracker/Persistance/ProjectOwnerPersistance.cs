using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Infrastructure.Persistance;
using Domain.Entities.Roles;
using Domain.Entities;

namespace BugTracker.Persistance
{
    public class ProjectOwnerPersistance : IProjectOwnerPersistance
    {
        private ApplicationDbContext _ctx;
        public ProjectOwnerPersistance(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public async Task<ProjectOwner> GetProjectOwner(string userId)
        {
            return await _ctx.ProjectOwner.FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public ProjectOwner GetProjectOwnerWithRelatedProjects(string userId)
        {
            //IQueryable<ProjectOwner> result1 = _ctx.ProjectOwner;
            //var result1List = result1.ToList();

            ////IQueryable<ProjectOwner> result2 = result1.Include(e=>e.Projects);
            ////var result2List = result2.ToList();

            //IQueryable<ProjectOwner> result3 = result1.Where(item => item.UserId == userId).Include(e => e.Projects);
            //var result3List = result3.ToList();
            return _ctx.ProjectOwner
             .Include(po => po.Projects)
             .Where(e => e.UserId == userId)
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
