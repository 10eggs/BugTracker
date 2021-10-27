using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.RequestItems.Queries.GetInProcessRequestItemDetails;
using Application.RequestItems.Queries.GetInProcessRequestItems;
using BugTracker.Persistance;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BugTrackerWeb.Pages.UserView
{
    public class InProcessRequestsModel : PageModel
    {
        private readonly IMediator mediator;
        private readonly ITicketPersistance _tp;
        public InProcessRequestsModel(IMediator mediator, ITicketPersistance tp)
        {
            this.mediator = mediator;
        }
        //[BindProperty]
        //public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public Ticket CheckedTicket { get; set; }

        //[BindProperty]
        //public GetInProcessRequestItemsQuery GetInProcessRequestItemsQuery { get; set; }

        [BindProperty]
        public InProcessRequestItemsVm InProcessRequestItemsVm { get; set; }

        public async Task OnGet()
        {
            InProcessRequestItemsVm = await mediator.Send(new GetInProcessRequestItemsQuery());

        }

        public async Task<PartialViewResult> OnGetTicketDetails(int id)
        {
            InProcessRequestItemsVm = await mediator.Send(new GetInProcessRequestItemDetailsQuery { TicketId = id });

            return new PartialViewResult
            {
                ViewName = "_InProcessTicketDetails",
                ViewData = new ViewDataDictionary<InProcessRequestItemsVm>(ViewData, InProcessRequestItemsVm)
            };
        }
    }
}
