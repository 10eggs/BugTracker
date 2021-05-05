using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTickets
{
    public class TicketsVm
    {
        public IList<TicketDto> Tickets { get; set; }
    }
}
