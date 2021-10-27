using Application.Common.Interfaces;
using Application.RequestItems.Queries.GetInProcessRequestItems;
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

namespace Application.RequestItems.Queries.GetInProcessRequestItemDetails
{
    public class GetInProcessRequestItemDetailsQuery:IRequest<InProcessRequestItemsVm>
    {
        public int TicketId { get; set; }
    }

    public class GetInProcessRequestItemDetailsQueryHandler : IRequestHandler<GetInProcessRequestItemDetailsQuery,
        InProcessRequestItemsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        public GetInProcessRequestItemDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<InProcessRequestItemsVm> Handle(GetInProcessRequestItemDetailsQuery request, CancellationToken cancellationToken)
        {
            return new InProcessRequestItemsVm
            {
                InProcessRequests = await context.Tickets
                    .Where(r => r.Id == request.TicketId)
                    .ProjectTo<InProcessRequestItemDto>(mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }
}
