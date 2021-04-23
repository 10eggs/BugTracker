using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Command.AssignTicket
{
    class AssignTicketCommand:IRequest<int>
    {
    }

    public class CreateRequestItemCommandHandler : IRequestHandler<AssignTicketCommand, int>
    {
        public Task<int> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
