using Domain.Common;
using Domain.Entities.Roles;
using Domain.Enums.Ticket;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ticket : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        //These need to be removed
        public string Title { get; set; }
        public string StepsToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public string Author { get; set; }
        //

        public RequestItem RequestItem { get; set; }
        public int RequestItemId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public TicketSeverity TicketSeverity { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string TicketAuthorEmail { get; set; }
        public string TicketAuthorId { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public QA Qa { get; set; }
        public int QaID { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
