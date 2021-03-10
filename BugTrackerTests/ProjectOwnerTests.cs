using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using System;
using System.Linq;
using System.Text;
using Xunit;
using static Xunit.Assert;
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

        [Fact]
        public void GetProjectOwnerWithRelatedProjects()
        {

            ProjectOwner po = new ProjectOwner() {Id=1 ,UserId = "1" };
            Project proj1 = new Project() { ProjectOwnerId = 1 , Description="desc1"};
            Project proj2 = new Project() { ProjectOwnerId = 1 , Description="des2"};

            using (var db = DbContextFactory.Create(nameof(GetProjectOwnerWithRelatedProjects)))
            {
                IProjectOwnerPersistance pop = new ProjectOwnerPersistance(db);
                pop.SaveAsync(po);
            }

            using (var db = DbContextFactory.Create(nameof(GetProjectOwnerWithRelatedProjects)))
            {
                IProjectPersistance pp = new ProjectPersistance(db);
                pp.Save(proj1);
                pp.Save(proj2);
            }
            using (var db = DbContextFactory.Create(nameof(GetProjectOwnerWithRelatedProjects)))
            {
                IProjectOwnerPersistance pop = new ProjectOwnerPersistance(db);
                var poRetrived = pop.GetProjectOwnerWithRelatedProjects("1");
                Assert.Equal("1", poRetrived.UserId);
                Assert.Equal(2, poRetrived.Projects.Count);

            }
        }
        [Fact]
        public void GetProjectOwner()
        {

            ProjectOwner po = new ProjectOwner() { UserId = "1"};
            using (var db = DbContextFactory.Create(nameof(GetProjectOwner)))
            {
                IProjectOwnerPersistance pop = new ProjectOwnerPersistance(db);
                pop.SaveAsync(po);
            }
            using (var db = DbContextFactory.Create(nameof(GetProjectOwner)))
            {
                IProjectOwnerPersistance pop = new ProjectOwnerPersistance(db);
                var poRetrived = pop.GetProjectOwner("1");
                Assert.Equal("1", poRetrived.Result.UserId);

            }
        }
    }
}
