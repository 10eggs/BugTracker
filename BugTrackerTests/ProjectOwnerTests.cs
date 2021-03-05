using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static Xunit.Assert;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BugTrackerTests
{
    public class ProjectOwnerTests
    {
        [Fact]
        public void GetRelatedProjects()
        {
            //Arrange
            ProjectOwner po = new ProjectOwner();

            Project proj1 = new Project();
            proj1.ProjectOwner = po;

            Project proj2 = new Project();
            proj2.ProjectOwner = po;

            Ticket t1 = new Ticket("title", "des", "auth", proj1);
            Ticket t2 = new Ticket("title", "des", "auth", proj1);
            Ticket t3 = new Ticket("title", "des", "auth", proj2);

            using (var db = DbContextFactory.Create(nameof(GetRelatedProjects)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                tp.SaveTicket(t1);
                tp.SaveTicket(t2);
                tp.SaveTicket(t3);

                Debug.WriteLine(db.Project.Count());
                IProjectPersistance pp = new ProjectPersistance(db);

            }

            using (var db = DbContextFactory.Create(nameof(GetRelatedProjects)))
            {
                IProjectOwnerPersistance pop = new ProjectOwnerPersistance(db);
                var projs1 = pop.GetRelatedProjects(1);
                var projs2 = pop.GetRelatedProjects(2);

            }


        }
    }

    internal interface IProjectOwnerPersistance
    {
        public void Save(ProjectOwner po);
        List<Project> GetRelatedProjects(int poId);
    }

    public class ProjectOwnerPersistance : IProjectOwnerPersistance
    {
        private AppDbContext _ctx;
        public ProjectOwnerPersistance(AppDbContext context)
        {
            _ctx = context;
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

        public void Save(ProjectOwner po)
        {
            _ctx.ProjectOwner.Add(po);
            _ctx.SaveChanges();
        }
    }
}
