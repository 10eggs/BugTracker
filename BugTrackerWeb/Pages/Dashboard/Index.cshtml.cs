using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly ITicketPersistance tp;
        public IndexModel(AppDbContext ctx)
        {
            _ctx = ctx;
            tp = new TicketPersistance(ctx);
        }

        public List<Ticket> Tickets { get; set; }

        public async Task OnGet()
        {
            Tickets = await _ctx.Tickets.ToListAsync();
        }

        //Handler for Delete action
        //Task<IActionResult> redirect us to the same page
        public async Task<IActionResult> OnPostDelete(int id)
        {
            using (_ctx)
            {
                await tp.DeleteById(id);
            }
            return RedirectToPage("Index");
        }
    }
}
