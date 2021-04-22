using Application.Common.Interfaces;
using Application.RequestItems.Queries.GetPendingRequestItems;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetRequestItemDetails
{
    public class GetRequestItemDetailsQuery :IRequest<PendingRequestItemsListVm>
    {
        public int RequestId { get; set; }
        public int ProjectId { get; set; }
    }

    public class GetRequestItemDetailsQueryHandler : IRequestHandler<GetRequestItemDetailsQuery, PendingRequestItemsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetRequestItemDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PendingRequestItemsListVm> Handle(GetRequestItemDetailsQuery request, CancellationToken cancellationToken)
        {
            return new PendingRequestItemsListVm
            {
                Requests = await _context.Requests
                 .Where(r => r.Id == request.RequestId)
                 .ProjectTo<PendingRequestItemDto>(_mapper.ConfigurationProvider).ToListAsync()
            };

        }
    }
}
