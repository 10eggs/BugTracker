using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    class AssignedTicket:Ticket
    {
        public QA Qa { get; set; }
        public int QaID { get; set; }
        public string TicketStatus { get; set; }
        public string Updated { get; set; }
        public string TicketPriority { get; set; }
        public string TicketCategory { get; set; }

    }
}
