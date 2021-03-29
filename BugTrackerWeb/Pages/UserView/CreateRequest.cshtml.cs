using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.UserView
{
    public class CreateTicketModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly IRequestPersistance _rp;
        private readonly IProjectPersistance _pp;
        private readonly ITicketPersistance _tp;
        public CreateTicketModel(AppDbContext context, IRequestPersistance rp, IProjectPersistance pp, ITicketPersistance tp)
        {
            _ctx = context;
            _rp = rp;
            _pp = pp;
            _tp = tp;
        }


        [BindProperty]
        public Request NewRequest { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        [BindProperty]
        public List<SelectListItem> ProjectListDDL { get; set; }


        public void OnGet()
        {
            ProjectListDDL = PopulateProjectList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                AssignUserToRequest();
                await _rp.SaveAsync(NewRequest);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public void AssignUserToRequest()
        {
            NewRequest.Author = User.Identity.Name;
        }

        //Maybe it could be generic at some point?
        public List<SelectListItem> PopulateProjectList()
        {

            return _pp.GetAll().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name

            }).ToList();
        }
    }
}
