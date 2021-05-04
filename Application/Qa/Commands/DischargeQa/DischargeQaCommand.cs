using Application.Common.Interfaces;
using Domain.Entities.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Qa.Commands.DischargeQa
{
    public class DischargeQaCommand:IRequest<JsonResult>
    {
        public int QaId { get; set; }
        public int ProjectId { get; set; }
    }

    public class DischargeQaCommandHandler : IRequestHandler<DischargeQaCommand, JsonResult>
    {
        private readonly IApplicationDbContext _context;
        public DischargeQaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<JsonResult> Handle(DischargeQaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = _context.Project.Where(p => p.Id == request.ProjectId)
                    .Include(p=>p.QAs)
                    .SingleOrDefault();

                var qa = _context.QA.Where(qa => qa.Id == request.QaId).SingleOrDefault();

                ((IList<QA>)project.QAs).Remove(qa);

                await _context.SaveChangesAsync(cancellationToken);

                return new JsonResult(new { success = true, message = "User has been discharged from project" });
            }

            catch (DbUpdateException e)
            {
                return new JsonResult(new { success = false, message = "Ups! Something went wrong, please try again" });
            }

        }
    }
}
