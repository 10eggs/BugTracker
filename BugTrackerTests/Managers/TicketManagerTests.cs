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
        static QA qa = new QA() { Id = 1, Name = "QA Name" };
        static Ticket ticket = new Ticket() { Id = 1, Author = "email2@email.com", Description = "Description2", Title = "Title2", ProjectId = 1 };
        static AssignedTicket assignedTicket = new AssignedTicket() { Id = 2, Author = "email@email.com", Description = "Description", Title = "Title", ProjectId = 1, QaID = 1 };




        Project project = new Project(new List<Ticket>() {ticket}, new List<QA>() { qa }) { Id = 1, Description = "Description", Name = "ProjectName" };



        [Fact]
        public void AssignTicket()
        {
            //Arrange
            //var ticketPersistance = new Mock<ITicketPersistance>();
            //ticketPersistance.Setup(o => o.GetById(1)).Returns(ticket);
            //var qaPersistance = new Mock<IQAPersistance>();
            //qaPersistance.Setup(o => o.Get(1)).Returns(qa);

            //Act
            //Get ticket
            //Assign ticket
            using (var db = DbContextFactory.Create(nameof(AssignTicket)))
            {
                //Mocks changed for real objects
                ITicketManager tm = new TicketManager(db, new TicketPersistance(db), new QAPersistance(db));
                db.Project.Add(project);
                db.Tickets.Add(ticket);
                db.QA.Add(qa);

                db.SaveChanges();
                tm.AssignToQa(1, 1);
            }

            //Assert
            //Verify if ticket Has changed to Assigned in the project table
            //Check if ticket has an assigned QA
            //Check if QA has this ticket assigned
            using (var db = DbContextFactory.Create(nameof(AssignTicket)))
            {
                var t = (AssignedTicket)db.Tickets.Include(t=> ((AssignedTicket)t).Qa).Select(b => b).FirstOrDefault();
                //var assignedTicket = qaPersistance.Object.Get(1).Tickets;
                //Assert.Equal(assignedTicket.SingleOrDefault().Qa, qaPersistance.Object.Get(1));
                
            }
        }
    }

    public class TicketManager : ITicketManager
    {
        private AppDbContext _ctx;
        private ITicketPersistance _tp;
        private IQAPersistance _qap;
        public TicketManager(AppDbContext ctx,ITicketPersistance tp, IQAPersistance qap)
        {
            _ctx = ctx;
            _tp = tp;
            _qap = qap;

        }
        public void AssignToQa(int ticketId, int qaId)
        {
            var qa = _qap.Get(qaId);
            _tp.SaveAssigned(ticketId,qa);
            //var assignedTicket=_tp.GetById(qaId);
            //Add new instance of assigned ticket
            //qa.Tickets.Add((AssignedTicket)assignedTicket);
            _ctx.SaveChanges();

        }
    }

    public interface ITicketManager
    {
        public void AssignToQa(int ticketId, int qaId);


    }
}
