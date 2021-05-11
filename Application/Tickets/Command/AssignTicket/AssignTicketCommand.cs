using Application.Common.Interfaces;
using Application.RequestItems.Queries.GetPendingRequestItems;
using Domain.Entities;
using Domain.Enums.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tickets.Command.AssignTicket
{
    public class AssignTicketCommand:IRequest<int>
    {
        public TicketPriority TicketPriority { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public TicketSeverity TicketSeverity { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int QaID { get; set; }
        public int ProjectId { get; set; }
        public int RequestItemId { get; set; }

    }

    public class CreateRequestItemCommandHandler : IRequestHandler<AssignTicketCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public CreateRequestItemCommandHandler(IApplicationDbContext context,ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<int> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            var reqItem = await _context.Requests.FindAsync(request.RequestItemId);
            reqItem.Assigned = true;

            var entry = new Ticket();
            entry.RequestItem = reqItem;
            entry.TicketStatus = request.TicketStatus;
            entry.TicketPriority = request.TicketPriority;
            entry.TicketCategory = request.TicketCategory;
            entry.TicketSeverity = request.TicketSeverity;

            entry.TicketAuthorId = _currentUser.UserId;
            entry.TicketAuthorEmail = _currentUser.UserEmail;

            entry.ProjectId = reqItem.ProjectId;
            entry.QaID = request.QaID;

            await _context.Tickets.AddAsync(entry);
            await _context.SaveChangesAsync(cancellationToken);

            return reqItem.Id;
        }
    }
}
