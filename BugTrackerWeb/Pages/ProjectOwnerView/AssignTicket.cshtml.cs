using Application.Tickets.Command.AssignTicket;
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

        [BindProperty]
        public AssignTicketCommand AssignTicketCommand { get; set; }

        public async Task OnGet(int requestId, int projectId)
        {
            AssignTicketVm = await _mediator.Send(new GetAssignTicketQuery { RequestId = requestId, ProjectId = projectId});

        }
    }
}
