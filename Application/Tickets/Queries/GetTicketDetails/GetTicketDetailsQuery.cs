using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public class GetTicketDetailsQueryHandler : IRequestHandler<GetTicketDetailsQuery, TicketDetailsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTicketDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<TicketDetailsVm> Handle(GetTicketDetailsQuery request, CancellationToken cancellationToken)
        {

            return new TicketDetailsVm
            {
                Ticket = await context.Tickets
                    .Where(t => t.Id == request.TicketId)
                    .ProjectTo<TicketDto>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
            };
        }
    }
}
