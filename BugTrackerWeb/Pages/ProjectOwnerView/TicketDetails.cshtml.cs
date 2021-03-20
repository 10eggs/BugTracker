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
        private ITicketPersistance _tp;
        private IProjectPersistance _pp;
        private IQAManager _qam;
        public TicketDetailsModel(ITicketPersistance tp,IProjectPersistance pp,IQAManager qam)
        {
            _tp = tp;
            _qam = qam;
            _pp = pp;
        }
        
        [BindProperty]
        public Project Project { get; set; }
        public Ticket Ticket { get; set; }
        public string QaName { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }
        public void OnGet(int projectId, int ticketId)
        {
            Debug.WriteLine("Project id equal to "+ projectId);
            var t = ticketId;
            //Search for project with id, and then search for ticket from project
            Project = _pp.Get(projectId);
            Ticket = Project.Tickets
                .Where(t => t.Id == ticketId)
                .SingleOrDefault();
            //Search for Qas based on projectId from ROUTE
            AvailableQAs = Project.QAs;
        }

        public IActionResult OnPostAssignTicket(string qaName, int ticketId)
        {
            Debug.WriteLine("Qa name is " + qaName);
            return RedirectToPage("./ProjectOwnerView");
        }
    }
}
