using Application.Common.Interfaces;
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

namespace Application.Qa.Commands.AssignQa
{
    public class AssignQaCommand:IRequest<int>
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }
    }

    public class AssignCommandQaHandler : IRequestHandler<AssignQaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public AssignCommandQaHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AssignQaCommand request, CancellationToken cancellationToken)
        {
            var qa = await _context.QA.Where(qa => qa.Id == request.UserId).FirstOrDefaultAsync();

            var project = await _context.Project
                .Include(p=>p.QAs)
                .Where(project => project.Id == request.ProjectId).FirstOrDefaultAsync();

            ((IList<QA>)project.QAs).Add(qa);


            _context.Project.Update(project);

            await _context.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
    }
}
