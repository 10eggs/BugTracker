using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using Moq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;
using static Xunit.Assert;

namespace BugTrackerTests
{
    public class TicketPersistanceTest
    {
        private Ticket ticket = new Ticket("Title0", "Text0", "Name0");
        private Ticket ticketWithUniqueAuthor = new Ticket("Title0", "Text0", "Author");
        private Ticket ticketWithUniqueAuthor2 = new Ticket("Title0", "Text0", "Author");

        [Fact]
        public void SaveTicket()
        {
            using (var db = DbContextFactory.Create(nameof(SaveTicket)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                tp.SaveTicket(ticket);
            }

            using (var db = DbContextFactory.Create(nameof(SaveTicket)))
            {
                var record = db.Tickets.Single();
                Equal(ticket.Title, record.Title);
            }
        }

        [Fact]
        public void GetAllTickets()
        {
            int numberOfSavedTickets;
            using (var db = DbContextFactory.Create(nameof(GetAllTickets)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                var tickets = TicketFactory.CreateListOfTickets();
                numberOfSavedTickets = tickets.Count;

                foreach (Ticket t in tickets)
                {
                    tp.SaveTicket(t);
                }
            }

            using (var db = DbContextFactory.Create(nameof(GetAllTickets)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                var numberOfRetrievedTickets = tp.GetAll();
                Equal(numberOfRetrievedTickets.Count, numberOfSavedTickets);
            }
        }


        [Fact]

        public void GetCreatedByAuthor()
        {
            using (var db = DbContextFactory.Create(nameof(GetCreatedByAuthor)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                tp.SaveTicket(ticket);
                tp.SaveTicket(ticketWithUniqueAuthor);
                tp.SaveTicket(ticketWithUniqueAuthor2);
            }

            using (var db = DbContextFactory.Create(nameof(GetCreatedByAuthor)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                var ticketWithAuthor = tp.GetCreatedByAuthor("Author");

                Equal(2, ticketWithAuthor.Count);
                Equal("Author", ticketWithAuthor.FirstOrDefault().Author);
            }
        }

        [Fact]

        public void GetAssignedToTheProject()
        {
            //Arrange
            var project1 = new Project { Name = "Project1" };
            var project2 = new Project { Name = "Project2" };

            var ticketForProjOne = new Ticket("Title0", "Text0", "Name0", project1);
            var firstTicketFoProjTwo = new Ticket("Title1", "Text1", "Name0", project2);
            var secTicketForProjTwo = new Ticket("Title2", "Text2", "Name0", project2);


            ////Act
            using (var db = DbContextFactory.Create(nameof(GetAssignedToTheProject)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                tp.SaveTicket(ticketForProjOne);

                //Reference to the Project is here, but it's not under next invocation
                var rec = db.Tickets.Select(b => b).ToList().FirstOrDefault();

                tp.SaveTicket(firstTicketFoProjTwo);
                tp.SaveTicket(secTicketForProjTwo);
            }

            //Assert
            using (var db = DbContextFactory.Create(nameof(GetAssignedToTheProject)))
            {
                ITicketPersistance tp = new TicketPersistance(db);

                //Reference to the object is no longer present

                var assignedToTheProjectOne = tp.GetAssignedToProject("Project1");
                var assignedToTheProjecTwo = tp.GetAssignedToProject("Project2");

                Assert.Collection(assignedToTheProjectOne,
                    item1 => Assert.Equal("Project1", item1.Project.Name)
                    );
                Assert.Collection(assignedToTheProjecTwo,
                    item1=>Assert.Equal("Project2",item1.Project.Name),
                    item2=>Assert.Equal("Project2",item2.Project.Name)
                    );
            }
        }

        [Fact]
        public async void GetById()
        {
            //Arrange
            var project1 = new Project { Name = "Project1" };
            var ticketForProjOne = new Ticket("Title0", "Text0", "Name0", project1);

            using (var db = DbContextFactory.Create(nameof(GetById)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                await tp.Save(ticketForProjOne);
            }

            using (var db = DbContextFactory.Create(nameof(GetById)))
            {
                ITicketPersistance tp = new TicketPersistance(db);
                var ticket = await tp.GetById(1);

                Assert.NotNull(ticket);
                Assert.Equal("Title0", ticket.Title);
            }
        }

        [Fact]
        public async void EditTicket()
        {
            var project1 = new Project { Name = "Project1" };
            var project2 = new Project { Name = "Project2" };
            var ticketForProjOne = new Ticket("Title0", "Text0", "Name0", project1);

            using (var db = DbContextFactory.Create(nameof(EditTicket)))
            {
                var tp = new TicketPersistance(db);
                await tp.Save(ticketForProjOne);
            }

            using (var db = DbContextFactory.Create(nameof(EditTicket)))
            {
                Debug.WriteLine("Ticket for project one id: " + ticketForProjOne.Id);
                var tp = new TicketPersistance(db);
                var t = await tp.GetById(1);
                tp.Edit(t, "TitleChanged", "TextChanged");
            }

            using (var db = DbContextFactory.Create(nameof(EditTicket)))
            {
                var tp = new TicketPersistance(db);
                var t= await tp.GetById(1);
                Assert.Equal("TitleChanged", t.Title);
            }

        }


    }
}

