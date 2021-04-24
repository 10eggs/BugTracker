using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQuery :IRequest<AllProjectsVm>
    {
        //This feature will be rebuilded, to get only projects assigned to the particular project owner.
    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, AllProjectsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllProjectsQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<AllProjectsVm> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return new AllProjectsVm
            {
                Projects = _context.Project
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider).ToList()
            };
        }
    }
}
