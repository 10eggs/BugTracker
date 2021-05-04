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

namespace Application.Qa.Queries.GetAvailableQas
{
    public class GetAvailableQasRequest:IRequest<AvailableQasVm>
    {
        public int ProjectId { get; set; }
    }

    public class GetAvailableQasRequestHandler : IRequestHandler<GetAvailableQasRequest, AvailableQasVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAvailableQasRequestHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AvailableQasVm> Handle(GetAvailableQasRequest request, CancellationToken cancellationToken)
        {

            return new AvailableQasVm
            {
                AvailableQas = await _context.QA.Where(qa => qa.Projects.All(p => p.Id != request.ProjectId))
                .ProjectTo<QaDto>(_mapper.ConfigurationProvider).ToListAsync(),

                ProjectId = request.ProjectId

            };
            
        }
    }
}
