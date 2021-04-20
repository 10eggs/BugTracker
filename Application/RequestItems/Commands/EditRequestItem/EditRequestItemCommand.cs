using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Commands.EditRequestItem
{
    public class EditRequestItemCommand:IRequest
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepsToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public int ProjectId { get; set; }
    }

    public class EditRequestItemCommandHandler : IRequestHandler<EditRequestItemCommand>
    {
        private readonly IApplicationDbContext _context;
        public EditRequestItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(EditRequestItemCommand request, CancellationToken cancellationToken)
        {
            //Ugly, have to be chaged
            var requestItem = await _context.Requests.FindAsync(request.Id);
            if(request.Title!=null)
                requestItem.Title = request.Title;
            if(request.StepsToReproduce!=null)
                requestItem.StepsToReproduce = request.StepsToReproduce;
            if(request.ExpectedResult!=null)
                requestItem.ExpectedResult = request.ExpectedResult;
            if(request.ActualResult!=null)
                requestItem.ActualResult = request.ActualResult;
            requestItem.ProjectId = request.ProjectId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
