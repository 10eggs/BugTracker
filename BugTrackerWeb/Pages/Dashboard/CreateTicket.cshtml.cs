using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Persistance;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.Dashboard
{
    public class CreateTicketModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private  readonly ITicketPersistance tp;
        public CreateTicketModel(AppDbContext context)
        {
            _ctx = context;
            tp = new TicketPersistance(_ctx);

        }

        
        [BindProperty]
        public Ticket NewTicket { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                using (var db = _ctx)
                {
                    var tp = new TicketPersistance(_ctx);
                    await tp.Save(NewTicket);
                    return RedirectToPage("Index");
                }

            }
            else
            {
                return Page();
            }
        }
    }
}
