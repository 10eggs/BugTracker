using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace BugTrackerTests
{
    public class DbContextTests
    {
        [Fact]
        public void TicketCanBeSaved()
        {
            var ticket = TicketFactory.CreateSingleTicket();
            using (var ctx = DbContextFactory.Create(nameof(TicketCanBeSaved)))
            {
                ctx.Tickets.Add(ticket);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(TicketCanBeSaved)))
            {
                var record = ctx.Tickets.Single();
                Assert.Equal(ticket.Title, record.Title);
                Assert.Equal(ticket.Author, record.Author);
                Assert.Equal(ticket.Description, record.Description);
                Assert.Equal(ticket.Date, DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", null));
            }
        }

        [Fact]
        public void IncrementTestId()
        {
            var ticket = TicketFactory.CreateSingleTicket();
            var ticket2 = TicketFactory.CreateSingleTicket();

            using (var ctx = DbContextFactory.Create(nameof(IncrementTestId)))
            {
                ctx.Tickets.Add(ticket);
                ctx.Tickets.Add(ticket2);
                ctx.SaveChanges();

                var id1 = ctx.Entry(ticket).Property("Id").CurrentValue;
                var id2 = ctx.Entry(ticket2).Property("Id").CurrentValue;

                Assert.Equal(1, id1);
                Assert.Equal(2, id2);
            }
        }
    }
}
