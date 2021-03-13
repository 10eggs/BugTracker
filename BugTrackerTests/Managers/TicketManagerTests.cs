using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
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
        static AssignedTicket ticket1 = new AssignedTicket() { Id = 1, Author = "email@email.com", Description = "Description", Title = "Title", ProjectId = 1 };
        static Ticket ticket2 = new Ticket() { Id = 2, Author = "email2@email.com", Description = "Description2", Title = "Title2", ProjectId = 1 };

        Project project = new Project(new List<Ticket>() { ticket1, ticket2 }, new List<QA>() { qa }) { Id = 1, Description = "Description", Name = "ProjectName" };



        [Fact]
        public void AssignTicket()
        {
            //Arrange

            var ticketPersistance = new Mock<ITicketPersistance>();
            ticketPersistance.Setup(o => o.GetById(1)).Returns(ticket1);
            var qaPersistance = new Mock<IQAPersistance>();
            qaPersistance.Setup(o => o.Get(1)).Returns(qa);

            //Act
            //Get ticket
            //Assign ticket
            using (var db = DbContextFactory.Create(nameof(AssignTicket)))
            {
                ITicketManager tm = new TicketManager(db, ticketPersistance.Object, qaPersistance.Object);
                tm.AssignToQa(1, 1);
            }

            //Assert
            //Verify if ticket Has changed to Assigned in the project table
            //Check if ticket has an assigned QA
            //Check if QA has this ticket assigned
            using (var db = DbContextFactory.Create(nameof(AssignTicket)))
            {
                var assignedTicket = qaPersistance.Object.Get(1).Tickets;
                Assert.Equal(assignedTicket.SingleOrDefault().Qa, qaPersistance.Object.Get(1));
                
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
            _tp.SaveAssigned(ticketId);
            var assignedTicket=_tp.GetById(qaId);

            var qa = _qap.Get(qaId);
            //Add new instance of assigned ticket
            qa.Tickets.Add((AssignedTicket)assignedTicket);
            _ctx.SaveChanges();

        }
    }

    public interface ITicketManager
    {
        public void AssignToQa(int ticketId, int qaId);


    }
}
