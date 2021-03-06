using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Tickets.Command.AddComment;
using Application.Tickets.Queries.GetTicketDetails;
using BugTracker.Models.TicketProperties;
using BugTracker.PageManagers;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using BugTracker.Utils;
using Domain.Entities;
using Domain.Entities.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class TicketDetailsModel : PageModel
    {
        //Remove this line
        private readonly IProjectPersistance _pp;
        private readonly ITicketManager _itm;
        private readonly IRequestPersistance _rp;
        private readonly ITicketPersistance _tp;
        private readonly IMediator _mediator;
        public TicketDetailsModel(IProjectPersistance pp,ITicketManager itm, IRequestPersistance rp,ITicketPersistance tp,IMediator mediator)
        {
            _itm = itm;
            _pp = pp;
            _rp = rp;
            _tp = tp;
            _mediator = mediator;
            //This assignment have to be changed!!!
            //TicketCatDDLOptions = new SelectList(EnumUtil.GetValues<TicketCategory>());
        }

        [BindProperty]
        public RequestItem Request { get; set; }

        [BindProperty]
        public Ticket Ticket { get; set; }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public int RequestId { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }
        
        [BindProperty]
        public int QAId { get; set; }

        public SelectList TicketCatDDLOptions { get; set; }
        public SelectList TicketPriorDDLOptions { get; set; }
        public SelectList TicketStatDDLOptions { get; set; }

        public List<SelectListItem> QAsList { get; set; }

        public void OnGetRequestDetails(int projectId, int requestId)
        {
            //Check if FromQuery works
            //Checked and works
            //Rather than getting this request from requests pool, try to get it from projects pool in the future
            Request = _rp.GetById(requestId);
            Project = _pp.Get(projectId);
            ProjectId = Project.Id;
            RequestId = requestId;

            AvailableQAs = Project.QAs.ToList();

            QAsList = AvailableQAs.Select(qa => new SelectListItem
            {
                Value = qa.Id.ToString(),
                Text = qa.Name
            }).ToList();

            TicketCatDDLOptions = new SelectList(EnumUtil.GetValues<TicketCategory>());
            TicketPriorDDLOptions = new SelectList(EnumUtil.GetValues<TicketPriority>());
            TicketStatDDLOptions = new SelectList(EnumUtil.GetValues<TicketStatus>());
        }

        /**
         * This is version with razor forms
         * 
         */
        public async Task<IActionResult> OnPostAssignTicketFromForm()
        {

            if (!ModelState.IsValid)
            {
                return RedirectToPage($"/ProjectOwnerView/");

            }
            await _itm.AssignTicket(RequestId, Ticket);

            return RedirectToPage($"/ProjectOwnerView/Index");
        }
        /**
         * AJAX call for assignmnent 
         */
        public async Task<JsonResult> OnPostAssignTicket(int qaId, int ticketId)
        {
            _itm.AssignToQa(ticketId, qaId);

            //Validation here is required, implement it later
            return new JsonResult(new { success = true, message = "Ticket has been assigned!", errormessage = "Something went wrong, try again later" });

            //This Url.Action should be considered as a return type
            //return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });

        }


        //***********************************************//

        [BindProperty]
        public TicketDetailsVm TicketDetailsVm { get; set; }

        [BindProperty]
        public AddCommentCommand AddCommentCommand { get; set; } = new AddCommentCommand();

        public int TicketId { get; set; }
        public async Task OnGet(int ticketId)
        {
            TicketDetailsVm = await _mediator.Send(new GetTicketDetailsQuery { TicketId = ticketId });
        }

        public async Task<IActionResult> OnPost()
        {
            await _mediator.Send(AddCommentCommand);
            return RedirectToPage("/ProjectOwnerView/TicketDetails",new { ticketId=AddCommentCommand.TicketId });
        }

    }
}
