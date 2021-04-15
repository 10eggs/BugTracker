using Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetRequestItems
{
    public class GetRequestItemsQuery : IRequest<RequestItemsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetRequestItemsQuery(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

    }

    public class GetRequestItemsQueryHandler : IRequestHandler<GetRequestItemsQuery, RequestItemsListVm>
    {
        public async Task<RequestItemsListVm> Handle(GetRequestItemsQuery request, CancellationToken cancellationToken)
        {
            return new RequestItemsListVm();
        }
    }
}
