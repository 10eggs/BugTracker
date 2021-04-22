using Application.Common.Interfaces;
using Application.RequestItems.Queries.GetPendingRequestItems;
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

namespace Application.Tickets.Queries.GetAssignTicket
{
    public class GetAssignTicketQuery:IRequest<AssignTicketVm>
    {
        public int RequestId { get; set; }
    }

    public class GetAssignTicketQueryHandler : IRequestHandler<GetAssignTicketQuery, AssignTicketVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAssignTicketQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AssignTicketVm> Handle(GetAssignTicketQuery request, CancellationToken cancellationToken)
        {
            var requestItem =  _context.Requests
                 .Where(r => r.Id == request.RequestId)
                 .SingleOrDefault();

            return new AssignTicketVm
            {
                PendingRequestItemDto = _mapper.Map<PendingRequestItemDto>(requestItem)
            };

        }
    }
}
