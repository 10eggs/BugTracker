using BugTracker.DB;
using BugTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace BugTracker.Persistance
{
    public class ProjectPersistance : IProjectPersistance
    {
        private AppDbContext _ctx;
        public ProjectPersistance(AppDbContext context)
        {
            _ctx = context;
        }

        public Project Get(int id)
        {
            return _ctx.Project
                    .Include(e => e.ProjectOwner)
                    .Include(e => e.Tickets)
                    .Where(e=>e.Id==id)
                    .Single();
        }

        public Project GetProject(int id)
        {
            return _ctx.Project
                    .Include(e=>e.QAs)
                    .Where(e => e.Id == id)
                    .Single();
        }

        public List<Project> GetAll()
        {
            return _ctx.Project
                .Include(e => e.ProjectOwner)
                .Include(e => e.Tickets)
                .Select(x => x).ToList();
        }

        public List<QA> GetAssignedQAs(int projId)
        {
            return _ctx.Project.Include(p => p.QAs)
                .Where(p => p.Id == projId)
                .Select(p => p.QAs)
                .SingleOrDefault();
        }

        public List<Project> GetOwnedBy(ProjectOwner po)
        {
            return _ctx.Project
                .Include(e => e.ProjectOwner)
                .Include(e => e.Tickets)
                .Where(e => e.ProjectOwner.Name == po.Name)
                .ToList();
        }

        public List<Ticket> GetRelatedTickets(Project project)
        {
            //EF.Property<int>(e, "Id")
            return _ctx.Project
                .Include(e => e.ProjectOwner)
                .Include(e => e.Tickets)
                .Where(e => project.Name==e.Name)
                .SingleOrDefault()
                .Tickets;
        }

        public void Save(Project t)
        {
            _ctx.Project.Add(t);
            _ctx.SaveChanges();
        }

        public void AssignQa(int projectId, QA qa)
        {
            var proj = GetProject(projectId);
            proj.QAs.Add(qa);
            _ctx.SaveChanges();
        }
    }
}
