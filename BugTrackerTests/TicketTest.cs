using System;
using System.Diagnostics;
using Xunit;
using BugTracker.Models;
using BugTracker.Persistance;

namespace BugTrackerTests
{
    public class TicketTest
    {
        Ticket t = TicketFactory.CreateSingleTicket();
        Ticket t2 = TicketFactory.CreateSingleTicket();

        [Fact]
        public void TicketThrowWhenTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Ticket(null, "Text", "Name"));

        }

        [Fact]
        public void TicketThrowWhenTextIsNull()
        {
            Assert.Throws<ArgumentNullException>(()=> new Ticket("Title",null, "Name"));

        }

        [Fact]
        public void TicketThrowWhenAuthorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Ticket("Title","Text", null));
        }

        [Fact]
        public void TicketHasAText()
        {
            //a
            var ticket = t;
            //a
            Assert.NotNull(ticket.Description);

        }

        [Fact]
        public void TicketHasTheAuthor()
        {
            var ticket = t;
            Assert.NotNull(ticket.Author);
        }

        [Fact]
        public void ItemHasCorrectDate()
        {
            var ticket = t;
            var now = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", null);
            Assert.Equal(now, ticket.Date);
        }


        [Fact]
        public void TestEnums()
        {
            TestEnum.CheckType(Types.ONE);
        }
    }

    public enum Types
    {
        ONE,TWO,THREE
    }
    
    public class TestEnum
    {
        public static void CheckType(Types t)
        {
            var typeCode = t.ToString();
            Enum.Parse(typeof(Types), typeCode);
        }
    }

}
