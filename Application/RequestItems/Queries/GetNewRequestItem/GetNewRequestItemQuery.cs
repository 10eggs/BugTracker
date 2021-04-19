using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetNewRequestItem
{
    public class GetNewRequestItemQuery : IRequest<NewRequestItemVm>
    {
    }

    public class GetNewRequestItemQueryHandler:IRequestHandler<GetNewRequestItemQuery,NewRequestItemVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetNewRequestItemQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NewRequestItemVm> Handle(GetNewRequestItemQuery request, CancellationToken cancellationToken)
        {
            return new NewRequestItemVm
            {
                AvailableProjects = new Dictionary<int, string>(_context.Project
                .Select(p => new KeyValuePair<int, string>(p.Id, p.Name)))
        
            };
        }
    }
}
