using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;
using static Xunit.Assert;
using Microsoft.EntityFrameworkCore;


namespace BugTrackerTests
{
    public class ProjectPersistanceTest
    {
        private static List<Ticket> tickets = TicketFactory.CreateListOfTickets();
        private static ProjectOwner owner = new ProjectOwner();
        Project project1 = new Project(tickets) { Name = "ProjectName1", Description = "This is a project", ProjectOwner = owner };
        Project project2 = new Project(tickets) { Name = "ProjectName2", Description = "This is a project", ProjectOwner = owner };
        Project project3 = new Project(tickets) { Name = "ProjectName3", Description = "This is a project", ProjectOwner = owner };

        [Fact]
        public void SaveProject()
        {
            using (var db = DbContextFactory.Create(nameof(SaveProject)))
            {
                IProjectPersistance pp = new ProjectPersistance(db);
                pp.Save(project1);
            }

            using (var db = DbContextFactory.Create(nameof(SaveProject)))
            {
                IProjectPersistance pp = new ProjectPersistance(db);
                var record = pp.Get(1);

                Equal(project1.Name, record.Name);
                NotNull(record.Tickets);
                NotNull(record.ProjectOwner);
            }
        }

        [Fact]
        public void GetAll_ReturnsAllProjects()
        {
            using (var db = DbContextFactory.Create(nameof(GetAll_ReturnsAllProjects)))
            {
                IProjectPersistance tp = new ProjectPersistance(db);
                tp.Save(project1);
                tp.Save(project2);
                tp.Save(project3);
            }

            using (var db = DbContextFactory.Create(nameof(GetAll_ReturnsAllProjects)))
            {
                IProjectPersistance tp = new ProjectPersistance(db);
                var records = tp.GetAll();

                Assert.Collection(records,
                    item1 =>
                    {
                        Equal("ProjectName1", item1.Name);
                        NotNull(item1.ProjectOwner);
                        NotNull(item1.Tickets);
                    },

                    item2 =>
                    {
                        Assert.Equal("ProjectName2", item2.Name);
                        NotNull(item2.ProjectOwner);
                        NotNull(item2.Tickets);
                    },

                    item3 =>
                    {
                        Equal("ProjectName3", item3.Name);
                        NotNull(item3.ProjectOwner);
                        NotNull(item3.Tickets);
                    }
                    );
            }
        }

        [Fact]
        public void GetOwnedBy_ReturnProjectsOwnedByUser()
        {
            ProjectOwner po1 = new ProjectOwner { Name = "Name1" };
            ProjectOwner po2 = new ProjectOwner { Name = "Name2" };

            List<Project> projects = new List<Project>()
            {
                new Project(tickets) { Name = "ProjectName1", Description = "This is a project1", ProjectOwner = po1 },
                new Project(tickets) { Name = "ProjectName2", Description = "This is a project2", ProjectOwner = po1 },
                new Project(tickets) { Name = "ProjectName3", Description = "This is a project3", ProjectOwner = po2 },
                new Project(tickets) { Name = "ProjectName4", Description = "This is a project4", ProjectOwner = po2 },
                new Project(tickets) { Name = "ProjectName5", Description = "This is a project5", ProjectOwner = po2 },
            };


            using (var db = DbContextFactory.Create(nameof(GetOwnedBy_ReturnProjectsOwnedByUser)))
            {
                IProjectPersistance tp = new ProjectPersistance(db);
                foreach(var p in projects)
                {
                    tp.Save(p);
                }
            }

            using (var db = DbContextFactory.Create(nameof(GetOwnedBy_ReturnProjectsOwnedByUser)))
            {
                IProjectPersistance tp = new ProjectPersistance(db);
                var ownedByPo1 = tp.GetOwnedBy(po1);
                var ownedByPo2= tp.GetOwnedBy(po2);

                Equal(2,ownedByPo1.Count);
                Equal(3,ownedByPo2.Count);


                Assert.Collection(ownedByPo1,
                    project1 =>
                    {
                        Equal("ProjectName1", project1.Name);
                        NotNull(project1.Tickets);
                    },
                    project2 =>
                    {
                        Equal("ProjectName2", project2.Name);
                        NotNull(project2.Tickets);
                    });

                Assert.Collection(ownedByPo2,
                    project1 =>
                    {
                        Equal("ProjectName3", project1.Name);
                        NotNull(project1.Tickets);
                    },
                    project2 =>
                    {
                        Equal("ProjectName4", project2.Name);
                        NotNull(project2.Tickets);
                    },
                    project3 =>
                    {
                        Equal("ProjectName5", project3.Name);
                        NotNull(project1.Tickets);
                    });
                }

        }

        [Fact]
        public void GetRelatedTickets()
        {
            ProjectOwner projOwn = new ProjectOwner();

            Project proj1 = new Project() { ProjectOwner = projOwn };

            var ticket1 = new Ticket("Title","Description","Author",proj1);
            var ticket2 = new Ticket("Title","Description","Author",proj1);

            using (var db = DbContextFactory.Create(nameof(GetRelatedTickets)))
            {
                IProjectPersistance pp = new ProjectPersistance(db);
                pp.Save(proj1);

                ITicketPersistance tp = new TicketPersistance(db);
                tp.SaveTicket(ticket1);
                tp.SaveTicket(ticket2);

            }
            using (var db = DbContextFactory.Create(nameof(GetRelatedTickets)))
            {
                IProjectPersistance pp = new ProjectPersistance(db);
                var relatedTickets = pp.GetRelatedTickets(proj1);

                NotNull(relatedTickets);
                Assert.Collection(relatedTickets,
                    t1 =>
                    {
                        Equal(ticket1.Title, t1.Title);
                    },
                    t2 =>
                    {
                        Equal(ticket2.Title, t2.Title);
                    });
            }
        }

    }

    class ProjectPersistance : IProjectPersistance
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
                    .Where(e=>EF.Property<int>(e, "Id")==id)
                    .Single();
        }

        public List<Project> GetAll()
        {
            return _ctx.Project
                .Include(e => e.ProjectOwner)
                .Include(e => e.Tickets)
                .Select(x => x).ToList();
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
    }

    public interface IProjectPersistance
    {
        //How to access record from db - by passing reference or value
        public void Save(Project t);
        public Project Get(int id);
        public List<Project> GetAll();
        public List<Project> GetOwnedBy(ProjectOwner author);
        public List<Ticket> GetRelatedTickets(Project project);
    }
}
