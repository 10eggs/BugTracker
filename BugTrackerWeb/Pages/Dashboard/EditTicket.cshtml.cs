using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.DB;
using BugTracker.Models;
using BugTracker.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.Dashboard
{
    public class EditTicketModel : PageModel
    {
        private readonly AppDbContext _ctx;
        private readonly ITicketPersistance tp;
        public EditTicketModel(AppDbContext context)
        {
            _ctx = context;
           

        }

        [BindProperty]
        public Ticket EditedTicket { get; set; }
        public async Task OnGet(int id)
        {
            using(_ctx)
            {
                var tp =new TicketPersistance(_ctx);
                EditedTicket = await tp.GetById(id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            //hidden input for id
            //Read about tags helpers
            if (ModelState.IsValid)
            {
                using (_ctx)
                {
                    var tp = new TicketPersistance(_ctx);
                    var et = await tp.GetById(EditedTicket.Id);

                    await tp.Edit(et, EditedTicket.Title, EditedTicket.Description);
                    await _ctx.SaveChangesAsync();
                }
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
