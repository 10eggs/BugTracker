using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestItems.Commands.CreateRequestItem
{
    public class CreateRequestItemCommand:IRequest<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string StepToReproduce { get; set; }
        [Required]
        public string ExpectedResult { get; set; }
        [Required]
        public string ActualResult { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }

    public class CreateRequestItemCommandHandler : IRequestHandler<CreateRequestItemCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreateRequestItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService )
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateRequestItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new RequestItem();
            entity.Title = request.Title;
            entity.StepsToReproduce = request.StepToReproduce;
            entity.ExpectedResult = request.ExpectedResult;
            entity.ActualResult = request.ActualResult;
            entity.ProjectId = request.ProjectId;
            entity.Author = _currentUserService.UserId;

            _context.Requests.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
