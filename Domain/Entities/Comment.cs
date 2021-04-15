using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
