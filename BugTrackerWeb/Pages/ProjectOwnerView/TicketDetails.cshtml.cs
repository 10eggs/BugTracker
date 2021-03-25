using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Models.TicketProperties;
using BugTracker.PageManagers;
using BugTracker.Persistance;
using BugTracker.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class TicketDetailsModel : PageModel
    {
        //Remove this line
        private IProjectPersistance _pp;
        private ITicketManager _itm;
        public TicketDetailsModel(IProjectPersistance pp,ITicketManager itm)
        {
            _itm = itm;
            _pp = pp;
            //This assignment have to be changed!!!
            TicketCatDDLOptions = new SelectList(EnumUtil.GetValues<TicketCategory>());
        }
        
        [BindProperty]
        public AssignedTicket AssignedTicket { get; set; }

        public SelectList TicketCatDDLOptions { get; set; }
        public SelectList TicketPriorDDLOptions { get; set; }
        public SelectList TicketStatDDLOptions { get; set; }


        public SelectList QAsList { get; set; }

        [BindProperty]
        public Project Project { get; set; }
        public Ticket Ticket { get; set; }
        
        //Test property for query parameter
        [FromQuery(Name = "projectId")]
        public string ProjectIdFromQuery { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }

        public void OnGetTicketDetails(int projectId, int ticketId)
        {
            //Check if FromQuery works
            //Checked and works
            Debug.WriteLine($"From query: {ProjectIdFromQuery}");

            Project = _pp.Get(projectId);
            Ticket = Project.Tickets
                .Where(t => t.Id == ticketId)
                .SingleOrDefault();

            AvailableQAs = Project.QAs;


            QAsList = new SelectList(AvailableQAs.Select(qa => qa.Name));
            TicketCatDDLOptions = new SelectList(EnumUtil.GetValues<TicketCategory>());
            TicketPriorDDLOptions = new SelectList(EnumUtil.GetValues<TicketPriority>());
            TicketStatDDLOptions = new SelectList(EnumUtil.GetValues<TicketStatus>());
        }
        public async Task<JsonResult> OnPostAssignTicket(int qaId, int ticketId)
        {
            _itm.AssignToQa(ticketId, qaId);

            //Validation here is required, implement it later
            return new JsonResult(new { success = true, message = "Ticket has been assigned!",errormessage="Something went wrong, try again later"});

            //This Url.Action should be considered as a return type
            //return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });

        }

        public async Task<IActionResult> OnPostAssignTicketFromForm()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var ticCat = AssignedTicket.TicketCategory;
            var ticPriority = AssignedTicket.TicketPriority;
            var ticStat = AssignedTicket.TicketStatus;

            Debug.WriteLine($"All captured values: {ticCat},{ticPriority},{ticStat}");
            return Page();
        }
    }
}
