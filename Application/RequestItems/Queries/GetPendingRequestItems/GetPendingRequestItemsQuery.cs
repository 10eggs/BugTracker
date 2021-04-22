using Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.RequestItems.Queries.GetPendingRequestItems
{
    public class GetPendingRequestItemsQuery : IRequest<PendingRequestItemsListVm>
    {

    }

    public class GetRequestItemsQueryHandler : IRequestHandler<GetPendingRequestItemsQuery, PendingRequestItemsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetRequestItemsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<PendingRequestItemsListVm> Handle(GetPendingRequestItemsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserRole.Equals("User"))
            {
                return new PendingRequestItemsListVm
                {
                    Requests = await _context.Requests
                    .Where(p => p.Author == _currentUserService.UserId)
                    .ProjectTo<PendingRequestItemDto>(_mapper.ConfigurationProvider).ToListAsync()
                };
            }
            return new PendingRequestItemsListVm
            {
                Requests = await _context.Requests
                .ProjectTo<PendingRequestItemDto>(_mapper.ConfigurationProvider).ToListAsync()
            };
        }
    }
}
