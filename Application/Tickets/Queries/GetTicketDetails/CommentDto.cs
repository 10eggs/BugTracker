using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTicketDetails
{
    public class CommentDto
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
