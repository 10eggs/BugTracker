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

namespace Application.RequestItems.Queries.GetInProcessRequestItems
{
    public class GetInProcessRequestItemsQuery:IRequest<InProcessRequestItemsVm>
    {

    }

    public class GetInProcessRequestItemsQueryHandler : IRequestHandler<GetInProcessRequestItemsQuery, InProcessRequestItemsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService userService;
        private readonly IMapper mapper;
        public GetInProcessRequestItemsQueryHandler(IApplicationDbContext context, ICurrentUserService userService, IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }
        public async Task<InProcessRequestItemsVm> Handle(GetInProcessRequestItemsQuery request, CancellationToken cancellationToken)
        {
            return new InProcessRequestItemsVm
            {
                InProcessRequests = await context.Tickets
                .Where(r => r.RequestItem.Assigned==true && r.RequestItem.Author == userService.UserId)
                .ProjectTo<InProcessRequestItemDto>(mapper.ConfigurationProvider).ToListAsync()
            };
        }
    }
}
