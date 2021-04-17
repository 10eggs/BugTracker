using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Commands.CreateRequestItem
{
    public class CreateRequestItemCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string StepToReproduce { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }
        public int ProjectId { get; set; }
    }

    public class CreateRequestItemCommandHandler : IRequestHandler<CreateRequestItemCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateRequestItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateRequestItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new RequestItem();
            entity.Title = request.Title;
            entity.StepsToReproduce = request.StepToReproduce;
            entity.ExpectedResult = request.ExpectedResult;
            entity.ActualResult = request.ActualResult;
            entity.ProjectId = request.ProjectId;

            _context.Requests.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
