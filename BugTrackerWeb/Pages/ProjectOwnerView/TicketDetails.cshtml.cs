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
        private ITicketPersistance _tp;
        private IQAManager _qam;
        public TicketDetailsModel(ITicketPersistance tp,IQAManager qam)
        {
            _tp = tp;
            _qam = qam;
        }
        
        [BindProperty]
        public Ticket Ticket { get; set; }
        public string QaName { get; set; }

        [BindProperty]
        public ICollection<QA> AvailableQAs { get; set; }
        public void OnGet(int id)
        {
            var t = id;
            Ticket = _tp.GetById(id);
            AvailableQAs = _qam.GetQAsForProject(Ticket.ProjectId);
        }

        public IActionResult OnPostAssignTicket(string qaName, int ticketId)
        {
            Debug.WriteLine("Qa name is " + qaName);
            return RedirectToPage("./ProjectOwnerView");
        }
    }
}
