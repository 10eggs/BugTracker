using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Command.AssignTicket
{
    public class AssignTicketCommand:IRequest<int>
    {
        public string TicketPriority { get; set; }
        public string TicketStatus { get; set; }
        public string TicketSeverity { get; set; }
    }

    public class CreateRequestItemCommandHandler : IRequestHandler<AssignTicketCommand, int>
    {
        public Task<int> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
