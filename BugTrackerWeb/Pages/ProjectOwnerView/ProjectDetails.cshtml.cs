using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.ProjectOwnerView
{
    public class ProjectDetailsModel : PageModel
    {
        private ITicketPersistance _tp;
        public ProjectDetailsModel(AppDbContext ctx, ITicketPersistance tp)
        {
            _tp = tp;
        }
        public int ProjectId { get; set; }

        public List<Ticket> ProjectTickets;
        public void OnGet(int id)
        {
            ProjectTickets = _tp.GetAssignedToProject(id);
            ProjectId = id;
        }
    }
}
