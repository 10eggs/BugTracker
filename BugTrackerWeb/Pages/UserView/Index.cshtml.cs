using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.UserView
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly ITicketPersistance _tp;
        private readonly IRequestPersistance _rp;
        public IndexModel(AppDbContext ctx, ITicketPersistance tp, IRequestPersistance rp)
        {
            _ctx = ctx;
            _tp = tp;
            _rp = rp;
        }

        public List<Request> Requests { get; set; }
        public List<Ticket> Tickets { get; set; }


        public async Task OnGet()
        {
            Requests =  _rp.GetCreatedByAuthor(User.Identity.Name);
            Tickets =  await _tp.GetByRequestAuthor(User.Identity.Name);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _rp.DeleteByIdAsync(id);
            return RedirectToPage("Index");
        }
    }
}

