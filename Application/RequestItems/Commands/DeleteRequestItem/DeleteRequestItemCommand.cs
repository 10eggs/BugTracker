using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Application.RequestItems.Commands.DeleteRequestItem
{
    public class DeleteRequestItemCommand : IRequest<JsonResult>
    {
        public int Id { get; set; }
    }

    class DeleteRequestItemCommandHandler : IRequestHandler<DeleteRequestItemCommand, JsonResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRequestItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> Handle(DeleteRequestItemCommand request, CancellationToken cancellationToken)
        {
            var requestItem = await _context.Requests.FindAsync(request.Id);
            if (requestItem == null)
            {
                throw new NullReferenceException($"Request item with id {request.Id} has not been found");
            }

            try
            {
                _context.Requests.Remove(requestItem);
                await _context.SaveChangesAsync(cancellationToken);
                return new JsonResult(new { success = true, message = "Record has been removed" });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine("Exception: " + ex);
                return new JsonResult(new { success = false, message = "Ups! Something went wrong. Please try again." });
            }
        }
    }
}
