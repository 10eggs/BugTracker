using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerWeb.Pages.UserView
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ITicketPersistance _tp;
        private readonly IProjectPersistance _pp;
        private readonly IRequestPersistance _rp;
        public IndexModel(ApplicationDbContext ctx, ITicketPersistance tp, IProjectPersistance pp, IRequestPersistance rp)
        {
            _ctx = ctx;
            _tp = tp;
            _pp = pp;
            _rp = rp;
        }

        public List<RequestItem> Requests { get; set; }
        public List<Ticket> Tickets { get; set; }

        [BindProperty]
        public RequestItem NewRequest { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        public List<SelectListItem> ProjectListDDL { get; set; }

        public async Task OnGet()
        {
            Requests =  _rp.GetCreatedByAuthor(User.Identity.Name);
            Tickets =  await _tp.GetByRequestAuthor(User.Identity.Name);
            ProjectListDDL = PopulateProjectList();
            Debug.WriteLine("Check me");

        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _rp.DeleteByIdAsync(id);
            return RedirectToPage("Index");
        }

        public void AssignUserToRequest()
        {
            NewRequest.Author = User.Identity.Name;
        }

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

