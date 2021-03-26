using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BugTrackerTests.Managers
{
    public class TicketManagerTests
    {
        //    static QA qa = new QA() { Id = 1, Name = "QA Name" };
        //    static QA qa2 = new QA() { Id = 2, Name = "QA Name2" };
        //    static Ticket ticket = new Ticket() { Id = 1, Author = "email1@email.com", Description = "Description1", Title = "Title1", ProjectId = 1 };
        //    static Ticket ticket2 = new Ticket() { Id = 2, Author = "email@email.com", Description = "Description2", Title = "Title2", ProjectId = 1 };
        //    static Ticket ticket3 = new Ticket() { Id = 3, Author = "email@email.com", Description = "Description3", Title = "Title3", ProjectId = 2 };
        //    static Ticket ticket4 = new Ticket() { Id = 4, Author = "email@email.com", Description = "Description3", Title = "Title4", ProjectId = 1 };


        //    static AssignedTicket assignedTicket = new AssignedTicket() { Id = 2, Author = "email@email.com", Description = "Description", Title = "Title", ProjectId = 1, QaID = 1 };


        //    Project project = new Project(new List<Ticket>() {ticket,ticket2}, new List<QA>() { qa,qa2 }) { Id = 1, Description = "Description", Name = "ProjectName" };
        //    Project project2 = new Project(new List<Ticket>() {ticket3}, new List<QA>() { qa ,qa2}) { Id = 2, Description = "Description", Name = "ProjectName2" };

        //    [Fact]
        //    public void AssignTicket()
        //    {
        //        //Arrange
        //        //var ticketPersistance = new Mock<ITicketPersistance>();
        //        //ticketPersistance.Setup(o => o.GetById(1)).Returns(ticket);
        //        //var qaPersistance = new Mock<IQAPersistance>();
        //        //qaPersistance.Setup(o => o.Get(1)).Returns(qa);

        //        //Act
        //        //Get ticket
        //        //Assign ticket
        //        using (var db = DbContextFactory.Create(nameof(AssignTicket)))
        //        {
        //            //Mocks changed for real objects
        //            ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));
        //            db.Project.Add(project);
        //            db.Tickets.Add(ticket);
        //            db.Tickets.Add(ticket2);
        //            db.QA.Add(qa);

        //            db.SaveChanges();
        //            tm.AssignToQa(1, 1);
        //            tm.AssignToQa(2, 1);
        //        }

        //        //Assert
        //        //Verify if ticket Has changed to Assigned in the project table
        //        //Check if ticket has an assigned QA
        //        //Check if QA has this ticket assigned
        //        using (var db = DbContextFactory.Create(nameof(AssignTicket)))
        //        {
        //            var t = (AssignedTicket)db.Tickets.Include(t => ((AssignedTicket)t).Qa)
        //                .Include(t => t.Project)
        //                .Select(b => b).FirstOrDefault();
        //            Assert.NotNull(t);

        //            var t1 = db.Tickets.OfType<AssignedTicket>().ToList();
        //            Assert.Equal(2, t1.Count);


        //        }
        //    }

        //    [Fact]
        //    public void GetAllAssignedToTheProject()
        //    {
        //        using (var db = DbContextFactory.Create(nameof(GetAllAssignedToTheProject)))
        //        {
        //            //Mocks changed for real objects
        //            ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));
        //            db.Project.Add(project);
        //            db.Tickets.Add(ticket);
        //            db.Tickets.Add(ticket2);
        //            db.QA.Add(qa);

        //            db.SaveChanges();
        //            tm.AssignToQa(1, 1);
        //            tm.AssignToQa(2, 1);
        //        }

        //        using (var db = DbContextFactory.Create(nameof(GetAllAssignedToTheProject)))
        //        {
        //            ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));
        //            var AllAssignedTickets = db.Tickets.OfType<AssignedTicket>().ToList();
        //            var tickets = tm.GetAllAssignedForProject(1);

        //            Assert.Equal(2, tickets.Count);
        //        }
        //    }

        //    [Fact]
        //    public void GetAllFromProjectAssignedToTheQa()
        //    {
        //        using (var db = DbContextFactory.Create(nameof(GetAllFromProjectAssignedToTheQa)))
        //        {
        //            //Mocks changed for real objects
        //            ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));

        //            db.QA.Add(qa);
        //            db.QA.Add(qa2);

        //            db.Project.Add(project);
        //            db.Project.Add(project2);

        //            db.Tickets.Add(ticket);
        //            db.Tickets.Add(ticket2);
        //            db.Tickets.Add(ticket3);
        //            db.Tickets.Add(ticket4);

        //            db.SaveChanges();
        //            tm.AssignToQa(1, 1);
        //            tm.AssignToQa(2, 1);
        //            tm.AssignToQa(4, 2);
        //        }
        //        using (var db = DbContextFactory.Create(nameof(GetAllFromProjectAssignedToTheQa)))
        //        {
        //            ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));
        //            var t = tm.GetAllFromProjectAssignedToQa(1, 1);
        //            Assert.Equal(2, t.Count);

        //        }
        //    }
        //}

        //public class TicketManager : ITicketManager
        //{
        //    private AppDbContext _ctx;
        //    private ITicketPersistance _tp;
        //    private IQAPersistance _qap;
        //    public TicketManager(AppDbContext ctx, ITicketPersistance tp, IQAPersistance qap)
        //    {
        //        _ctx = ctx;
        //        _tp = tp;
        //        _qap = qap;

        //    }
        //    public void AssignToQa(int ticketId, int qaId)
        //    {
        //        var qa = _qap.Get(qaId);
        //        _tp.SaveAssigned(ticketId, qa);
        //    }

        //    public ICollection<AssignedTicket> GetAllAssignedForProject(int projectId)
        //    {
        //        return _ctx.Tickets.OfType<AssignedTicket>()
        //             .Include(t => t.Qa)
        //             .Include(t => t.Project)
        //            .Where(t => t.ProjectId == projectId)
        //            .Select(t => t)
        //            .ToList();
        //    }

        //    public ICollection<AssignedTicket> GetAllFromProjectAssignedToQa(int projectId, int qaId)
        //    {
        //        return _ctx.Tickets.OfType<AssignedTicket>()
        //                 .Include(t => t.Qa)
        //                 .Include(t => t.Project)
        //                .Where(t => t.ProjectId == projectId && t.QaID == qaId)
        //                .Select(t => t)
        //                .ToList();
        //    }
        //}

        //public interface ITicketManager
        //{
        //    public void AssignToQa(int ticketId, int qaId);

        //    public ICollection<AssignedTicket> GetAllAssignedForProject(int projectId);

        //    public ICollection<AssignedTicket> GetAllFromProjectAssignedToQa(int projectId, int qaId);

    }
}
