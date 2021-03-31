using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.QAView
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly IProjectPersistance _pp;
        private readonly IModelDistributor _imd;
        public ProjectDetailsModel(IProjectPersistance pp, IModelDistributor imd)
        {
            _pp = pp;
            _imd = imd;
        }
        [BindProperty]
        public List<Ticket> TicketList { get; set; }

        [BindProperty(SupportsGet = true)]
        public Project Project { get; set; }

        //This version works
        public async Task OnGet([FromQuery] Project project, int projectId, int qaId)
        {
            var proj = _imd.GetData<List<Project>>("projects");
            TicketList = await _pp.GetRelatedTicketsAssignedToQa(projectId, qaId);
        }

        //public async Task OnGet([FromQuery] int project, int projectId, int qaId)
        //{
        //    var xxx = project;
        //    //TicketList = await _pp.GetRelatedTicketsAssignedToQa(projectId, qaId);
        //}


    }
}
