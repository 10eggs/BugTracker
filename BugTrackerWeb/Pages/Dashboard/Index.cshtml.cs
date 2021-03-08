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
        private readonly ITicketPersistance _tp;
        public IndexModel(AppDbContext ctx, ITicketPersistance tp)
        {
            _ctx = ctx;
            //tp = new TicketPersistance(ctx);
            _tp = tp;
        }

        public List<Ticket> Tickets { get; set; }

        //This need to be separated later, 
        public List<Ticket> UserTickets { get; set; }

        public async Task OnGet()
        {
            UserTickets = await _tp.GetCreatedByAuthorAsync(User.Identity.Name);
            Tickets = await _ctx.Tickets.ToListAsync();
        }

        //Handler for Delete action
        //Task<IActionResult> redirect us to the same page
        public async Task<IActionResult> OnPostDelete(int id)
        {
            using (_ctx)
            {
                await _tp.DeleteById(id);
            }
            return RedirectToPage("Index");
        }
    }
}
