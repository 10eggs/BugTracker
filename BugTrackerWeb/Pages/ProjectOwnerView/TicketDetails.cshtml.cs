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
        public SelectList QAsList { get; set; }

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

        [FromQuery(Name = "projectId")]
        public string ProjectIdFromQuery { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }

        //public IActionResult OnPost()
        //{
        //    return RedirectToPage("/ProjectOwnerView/Index");
        //}
        public void OnGetTicketDetails(int projectId, int ticketId)
        {
            //Check if FromQuery works
            //Checked and works
            Debug.WriteLine($"From query: {ProjectIdFromQuery}");


            //This two have been added for test purpose
            //Works as well
            //ProjectId = projectId;
            //TicketId = ticketId;
            TicketCatDDLOptions = new SelectList(EnumUtil.GetValues<TicketCategory>());
            Project = _pp.Get(projectId);
            Ticket = Project.Tickets
                .Where(t => t.Id == ticketId)
                .SingleOrDefault();

            AvailableQAs = Project.QAs;
            QAsList = new SelectList(AvailableQAs.Select(qa=>qa.Name));
        }

        //public IActionResult OnPostCheckAjax(int qaId)
        //{
        //    Debug.WriteLine("My name is Luka and qaid is "+qaId);
        //    return RedirectToPage("/ProjectOwnerView/Index");
        //}

        public async Task<JsonResult> OnPostCheckAjax(int qaId, int ticketId)
        {
            //HTTP stateless, model values are not here
            //********WEIRD BEHAVIOR - PROJECT ID VALUE DOES NOT AVAILABLE HERE*****************//
            //Debug.WriteLine("Check if assigned model values are still here.ProjId: " + ProjectId + ", TicketId" + TicketId);
            //Debug.WriteLine($"Ticket id is {ticketId} and qaId is {qaId}");


            _itm.AssignToQa(ticketId, qaId);

            return new JsonResult(new { Success = "HOlA"! });
            //return RedirectToPage("/ProjectOwnerView/Index");


        }

        public async Task<JsonResult> OnPostAssignTicket(int qaId, int ticketId)
        {
            _itm.AssignToQa(ticketId, qaId);
            Debug.WriteLine("Called from handler");

            //previous
            //return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });
            //return RedirectToPage("Index");
            return new JsonResult(new { redirectToUrl = Url.Action("action", "contoller") });

            //return RedirectToPage("/ProjectOwnerView");
        }
    }
}
