using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Queries.GetEditRequestItem
{
    public class GetEditRequestItemQuery: IRequest<EditRequestItemVm>
    {
        public int Id { get; set; }
    }

    public class GetEditRequestItemQueryHandler : IRequestHandler<GetEditRequestItemQuery, EditRequestItemVm>
    {
        private readonly IApplicationDbContext _context;
        public GetEditRequestItemQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EditRequestItemVm> Handle(GetEditRequestItemQuery request, CancellationToken cancellationToken)
        {
            return new EditRequestItemVm
            {
                Id = request.Id,

                AvailableProjects = new Dictionary<int, string>(_context.Project
                .Select(p => new KeyValuePair<int, string>(p.Id, p.Name)))
            };
        }
    }
}
