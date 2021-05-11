using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Tickets.Queries.GetTickets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.ProjectOwnerView
{
    public class TicketsModel : PageModel
    {
        private readonly IMediator _mediator;
        public TicketsModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public TicketsVm TicketsVm { get; set; }
        public async Task OnGet()
        {
            TicketsVm = await _mediator.Send(new GetTicketsQuery());
        }

        public async Task OnGetTicketDetails(int ticketId)
        {
            Debug.WriteLine($"Ticket id is {ticketId}");
        }
    }
}
