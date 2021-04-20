using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.RequestItems.Commands.EditRequestItem;
using Application.RequestItems.Queries.GetEditRequestItem;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.UserView
{
    public class EditTicketModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditTicketModel(IMediator mediator)
        {
            _mediator = mediator;

        }
        [BindProperty]
        public EditRequestItemVm EditRequestItemVm { get; set; }

        [BindProperty]
        public EditRequestItemCommand EditRequestItemCommand { get; set; }

        public async Task OnGet(int id)
        {
            EditRequestItemVm = await _mediator.Send(new GetEditRequestItemQuery { Id = id });
        }

        public async Task<IActionResult> OnPost()
        {
            await _mediator.Send(EditRequestItemCommand);
            return RedirectToPage("PendingRequests");

        }


    }
}
