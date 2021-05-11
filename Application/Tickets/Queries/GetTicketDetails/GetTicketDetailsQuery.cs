using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTicketDetails
{
    public class GetTicketDetailsQuery:IRequest<TicketDetailsVm>
    {
        public int TicketId { get; set; }
    }

    //public class GetTicketDetailsQueryHandler : IRequestHandler<int, TicketDetailsVm>
    //{
    //    private readonly IApplicationDbContext _context;

    //    public GetTicketDetailsQueryHandler(IApplicationDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public Task<TicketDetailsVm> Handle(int request, CancellationToken cancellationToken)
    //    {
    //        return new TicketDetailsVm
    //        {
    //            Ticket = _context.
    //        }
    //    }
    //}
}
