using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Command.AddComment
{
    public class AddCommentCommand:IRequest<int>
    {
        public int TicketId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }

    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService userService;
        public AddCommentCommandHandler(IApplicationDbContext context, ICurrentUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public async Task<int> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {

            await context.Comments.AddAsync(new Comment { TicketId = request.TicketId, Text = request.Text, Author= userService.UserEmail });


            await context.SaveChangesAsync(cancellationToken);

            var t = await context.Tickets.FindAsync(request.TicketId);


            return t.Id;
        }
    }
}
