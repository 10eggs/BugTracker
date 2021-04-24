using Application.Common.Interfaces;
using Application.RequestItems.Queries.GetPendingRequestItems;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Roles;
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

        public int ProjectId { get; set; }
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
            var requestItem =  await _context.Requests
                 .Where(r => r.Id == request.RequestId)
                 .SingleOrDefaultAsync();

            //IDictionary<int,string> qaList = new Dictionary<int,string>(_context.Project.Where(p => p.Id == request.ProjectId)
            //    .Select(p => p.QAs.Select(qa => new KeyValuePair<int,string>(qa.Id, qa.Name)).ToDictionary<int,string>));

            //IDictionary<int,string> qas = new Dictionary<int,string>(_context.Project.Where(p => p.Id == request.ProjectId)
            //    .Select(p => p.QAs)
            //   .Cast<QA>()
            //   .Select(qa=>new KeyValuePair<int,string>(qa.Id,qa.Name)));

            //.S)
            //IDictionary<int, string> qaList = new Dictionary<int, string>(_context.Project
            //.Select(p => new KeyValuePair<int, string>(p.Id, p.Name)));

            //IDictionary<int,string> qaList = new Dictionary<int,string>(_context.Project
            //    .Where(p=>p.Requests.Select()

            return new AssignTicketVm
            {
                PendingRequestItemDto = _mapper.Map<PendingRequestItemDto>(requestItem),
                QaList = new Dictionary<int, string>(_context.Project.Where(p => p.Id == request.ProjectId)
                    .Select(p => p.QAs)
                   .Cast<QA>()
                   .Select(qa => new KeyValuePair<int, string>(qa.Id, qa.Name)))
        };

        }
    }
}
