using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BugTrackerWeb.Pages.UserView
{
    public class InProcessRequestsModel : PageModel
    {
        private readonly ITicketPersistance _tp;
        public InProcessRequestsModel(ITicketPersistance tp)
        {
            _tp = tp;
        }
        [BindProperty]
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public Ticket CheckedTicket { get; set; }

        public async Task OnGet()
        {
            Tickets = await _tp.GetByRequestAuthor(User.Identity.Name);

        }

        public async Task<PartialViewResult> OnGetRequestTicketDetails(int id)
        {
            CheckedTicket = _tp.GetById(id);

            return new PartialViewResult
            {
                ViewName = "_TicketDetails",
                ViewData = new ViewDataDictionary<Ticket>(ViewData, CheckedTicket)
            };
        }
    }
}
