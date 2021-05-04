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

namespace Application.Projects.Queries.GetTeamMembersForProject
{
    public class GetTeamMembersForProjectRequest : IRequest<TeamMembersVm>
    {
        public int ProjectId { get; set; }
    }

    public class GetTeamMembersForProjectRequestHandler : IRequestHandler<GetTeamMembersForProjectRequest, TeamMembersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTeamMembersForProjectRequestHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TeamMembersVm> Handle(GetTeamMembersForProjectRequest request, CancellationToken cancellationToken)
        {
            return new TeamMembersVm
            {

                Qas = await _context.QA.Where(qa => qa.Projects.Any(p => p.Id == request.ProjectId))
                .ProjectTo<QaDto>(_mapper.ConfigurationProvider).ToListAsync(),

                ProjectId = request.ProjectId

            };
        }
    }
}
