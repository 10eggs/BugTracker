using Application.Common.Utils;
using Application.RequestItems.Queries.GetPendingRequestItems;
using Domain.Enums.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetAssignTicket
{
    public class AssignTicketVm
    {
        public IDictionary<int, string> TicketCategory => EnumUtils.EnumDictionary<TicketCategory>();

        public IDictionary<int,string> TicketPriority => EnumUtils.EnumDictionary<TicketPriority>();

        public IDictionary<int, string> TicketSeverity => EnumUtils.EnumDictionary<TicketSeverity>();

        public IDictionary<int, string> TicketStatus => EnumUtils.EnumDictionary<TicketStatus>();

        public IDictionary<int, string> QaList { get; set; }

        public PendingRequestItemDto PendingRequestItemDto { get; set; }
    }
}
