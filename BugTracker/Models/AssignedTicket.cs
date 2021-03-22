using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Models
{
    public class AssignedTicket:Ticket
    {
        public AssignedTicket(Ticket t,QA qa):base()
        {
            this.Title = t.Title;
            this.Description = t.Description;
            this.Author = t.Author;
            this.ProjectId = t.ProjectId;
            this.Project = t.Project;
            this.Qa = qa;
            this.QaID = qa.Id;
        }

        public AssignedTicket(Ticket t) : base()
        {
            this.Title = t.Title;
            this.Description = t.Description;
            this.Author = t.Author;
            this.ProjectId = t.ProjectId;
            this.Project = t.Project;

        }

        public AssignedTicket() : base()
        {

        }

        public QA Qa { get; set; }
        public int QaID { get; set; }
        public string TicketStatus { get; set; }
        public DateTime Updated { get; set; }
        public string TicketPriority { get; set; }
        public string TicketCategory { get; set; }

    }
}
