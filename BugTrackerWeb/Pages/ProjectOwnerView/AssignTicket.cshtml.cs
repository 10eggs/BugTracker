using Application.Tickets.Queries.GetAssignTicket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebUI.Pages.ProjectOwnerView
{
    public class AssignTicketModel : PageModel
    {
        private readonly IMediator _mediator;
        public AssignTicketModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public AssignTicketVm AssignTicketVm { get; set; }
        public async Task OnGet(int id)
        {
            AssignTicketVm = await _mediator.Send(new GetAssignTicketQuery { RequestId = id });

        }
    }
}
