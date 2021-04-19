using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.RequestItems.Commands.CreateRequestItem;
using Application.RequestItems.Queries.GetNewRequestItem;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Pages;

namespace BugTrackerWeb.Pages.UserView
{
    public class NewRequestModel : PageModel
    {
        private readonly IMediator _mediator;


        public NewRequestModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public NewRequestItemVm NewRequestItemVm { get; set; }

        [BindProperty]
        public CreateRequestItemCommand CreateRequestItemCommand { get; set; }
        public async Task OnGetAsync()
        {

            NewRequestItemVm = await _mediator.Send(new GetNewRequestItemQuery());

        }
        public async Task<IActionResult> OnPostSaveRequest()
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(CreateRequestItemCommand);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }


}
