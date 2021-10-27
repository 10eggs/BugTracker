using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Queries.GetTickets
{
    public class GetTicketsQuery:IRequest<TicketsVm>
    {

    }

    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, TicketsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        public GetTicketsQueryHandler(IApplicationDbContext context,ICurrentUserService currentUser,IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }
        public async Task<TicketsVm> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            return new TicketsVm
            {
                Tickets = await _context.Tickets
                    .Where(t => t.Project.ProjectOwner.UserId == _currentUser.UserId)
                    .ProjectTo<TicketDto>(_mapper.ConfigurationProvider).ToListAsync()
            };
        }
    }
}
