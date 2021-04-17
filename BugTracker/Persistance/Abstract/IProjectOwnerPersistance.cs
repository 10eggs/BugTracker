using Domain.Entities;
using Domain.Entities.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Persistance
{
    public interface IProjectOwnerPersistance
    {
        public Task SaveAsync(ProjectOwner po);
        public Task<ProjectOwner> GetProjectOwner(string userId);
        public List<Project> GetRelatedProjects(int poId);
        public ProjectOwner GetProjectOwnerWithRelatedProjects(string userId);

    }
}
