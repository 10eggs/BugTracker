using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.PageManagers;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        }
        
        [BindProperty]
        public Project Project { get; set; }
        public Ticket Ticket { get; set; }
        public string QaName { get; set; }

        //Test properties
        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public int TicketId { get; set; }

        //Check this !
        //[FromQuery(Name = "foo-bar")]
        //public string FooBar { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }
        public void OnGet(int projectId, int ticketId)
        {
            //This two have been added for test purpose
            //ProjectId = projectId;
            //TicketId = ticketId;


            Project = _pp.Get(projectId);
            Ticket = Project.Tickets
                .Where(t => t.Id == ticketId)
                .SingleOrDefault();

            AvailableQAs = Project.QAs;
        }

        //public IActionResult OnPostCheckAjax(int qaId)
        //{
        //    Debug.WriteLine("My name is Luka and qaid is "+qaId);
        //    return RedirectToPage("/ProjectOwnerView/Index");
        //}

        public IActionResult OnPostCheckAjax(int qaId, int ticketId)
        {
            //HTTP stateless, model values are not here
            //********WEIRD BEHAVIOR - PROJECT ID VALUE DOES NOT AVAILABLE HERE*****************//
            //Debug.WriteLine("Check if assigned model values are still here.ProjId: " + ProjectId + ", TicketId" + TicketId);
            //Debug.WriteLine($"Ticket id is {ticketId} and qaId is {qaId}");


            _itm.AssignToQa(ticketId, qaId);

            return RedirectToPage("/ProjectOwnerView/Index");

        }

        public async Task<JsonResult> OnPostAssignTicket(int qaId, int ticketId)
        {
            _itm.AssignToQa(ticketId, qaId);
            //previous
            //return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });
            return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });

            //return RedirectToPage("/ProjectOwnerView");
        }
    }
}
