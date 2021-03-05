using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugTracker.Persistance
{
    public static class TicketFactory
    {
        static int ticketnum = 0;
        public static List<Ticket> CreateListOfTickets()
        {
            return Enumerable.Range(0, 5).Select(x => CreateSingleTicket()).ToList();
        }

        public static Ticket CreateSingleTicket()
        {
            ticketnum++;
            return new Ticket($"Title{ticketnum}", $"Text{ticketnum}", $"Name{ticketnum}");
        }
    }
}
